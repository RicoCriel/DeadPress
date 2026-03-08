using System;
using UnityEngine;
using UnityEngine.InputSystem;

//This class is responsible for controlling the different playerstates
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private PlayerBrain _brain;
    private PlayerView _view;

    public PlayerBrain Brain => _brain;
    public PlayerView View => _view;
    public bool IsGrounded => _isGrounded;

    public static event Action<Vector3> OnPlayerMoved;

    //Starting from here we will refactor in the next iteration
    [SerializeField] private bool _isGamepad;

    private Vector3 _moveInput = Vector3.zero;
    private bool _isGrounded;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _camera;

    private Quaternion _targetRotation;
    private bool _hasTargetRotation;
    private Vector3 _movementDirection;

    [Range(0, 50f)]
    [SerializeField] private float _gravity;
    
    private bool _isJumping;
    private float _jumpTimer = 0;
    [Range(0,5f)]
    [SerializeField] private float _maxJumpTime;

    private CharacterController _controller;

    private void Start()
    {
        _brain = GetComponent<PlayerBrain>();
        _view = GetComponent<PlayerView>();
        _controller = GetComponent<CharacterController>();
    }

    

    private void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, Brain.Locomotion.GroundDistance, Brain.Locomotion.Ground, QueryTriggerInteraction.Ignore);
        View.UpdateAnimationGroundedState(IsGrounded);

        _moveInput = Vector3.zero;
        _moveInput.x = Brain.Actions.Move.ReadValue<Vector2>().x;
        _moveInput.z = Brain.Actions.Move.ReadValue<Vector2>().y;
        _moveInput.Normalize();

        Move();

        if(_isJumping)
        {
            _jumpTimer += Time.deltaTime;
        }

        if(_jumpTimer > _maxJumpTime)
        {
            _isJumping = false;
            _jumpTimer = 0;
        }


        SetJumpRequest();
        View.UpdateSlidingAnimation(Brain);
        View.UpdateAimingAnimation(Brain);
        View.UpdateThrowingAnimation(Brain);

        //sends the current location to listeners
        OnPlayerMoved?.Invoke(transform.position);
    }

    private void Move()
    {
        _movementDirection = new Vector3(_moveInput.x, 0, _moveInput.z);
        float inputMagnitude = Mathf.Clamp01(_movementDirection.magnitude);
        _movementDirection = Quaternion.AngleAxis(_camera.rotation.eulerAngles.y, Vector3.up) * _movementDirection;
        _movementDirection.Normalize();

        View.UpdateWalkingAnimationMagnitude(inputMagnitude);

        if (_movementDirection != Vector3.zero)
        {
            View.UpdateWalkingAnimation(true);
            Quaternion targetRotation = Quaternion.LookRotation(_movementDirection, Vector3.up);
            _targetRotation = targetRotation;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 720f * Time.deltaTime);
        }
        else
        {
            View.UpdateWalkingAnimation(false); 
            _hasTargetRotation = false;
        }
    }

    private void OnAnimatorMove()
    {
        Vector3 velocity = View.Animator.deltaPosition / Time.deltaTime;
        _controller.Move(velocity * Time.deltaTime);
    }

    private void SetJumpRequest()
    {
        if (Brain.Actions.Jump.WasPressedThisFrame() && _isGrounded)
        {
            _isJumping = true;
        }
    }

    private float GetGravity()
    {
        return _movementDirection.y -= _gravity * Time.fixedDeltaTime;
    }
    public void OnDeviceChanged(PlayerInput playerInput)
    {
        _isGamepad = playerInput.currentControlScheme.Equals("Gamepad") ? true : false;
    }

    public void DisablePlayerControls()
    {
        Brain.Actions.DisableControls();
    }

    public void EnablePlayerControls()
    {
        Brain.Actions.EnableControls();
    }
}

