using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Player", menuName = "Player/InputActions", order = 0)]
public class PlayerInputActions : ScriptableObject
{
    public InputAction Move => _move;
    public InputAction Interact => _interact;
    public InputAction Look => _look;
    public InputAction Aim => _aim; 
    public InputAction Slide => _slide;
    public InputAction Jump => _jump;
    public InputAction Throw => _throw;

    private InputAction _move;
    private InputAction _interact;
    private InputAction _aim;
    private InputAction _look;
    private InputAction _slide;
    private InputAction _jump;
    private InputAction _throw;

    private PlayerControls _playerControls;

    private void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();
            InitializeActions();
        }

        _playerControls.Enable();
    }

    private void OnDisable()
    {
        if (_playerControls != null)
        {
            _playerControls.Disable();
        }
    }

    private void InitializeActions()
    {
        _move = _playerControls.Gameplay.Move;
        _interact = _playerControls.Gameplay.Interact;
        _aim = _playerControls.Gameplay.Aim;
        _look = _playerControls.Gameplay.Look;
        _slide = _playerControls.Gameplay.Slide;
        _jump = _playerControls.Gameplay.Jump;
        _throw = _playerControls.Gameplay.Throw;
    }

    public void DisableControls()
    {
        _playerControls.Disable();
    }

    public void EnableControls()
    {
        _playerControls.Enable();
    }
}
