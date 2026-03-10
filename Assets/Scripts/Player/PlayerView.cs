using UnityEngine;

public class PlayerView : MonoBehaviour
{
    //This class is only responsible for updating the visuals of the player
    private Animator _animator;
    public Animator Animator => _animator;
    
    private const float _dampTime = 0.05f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void UpdateWalkingAnimation(float speed)
    {
        _animator.SetFloat("Speed", speed );
    }

    public void UpdateSlidingAnimation(PlayerBrain brain)
    {
        if (brain.Actions.Slide.WasPressedThisFrame())
        {
            Animator.SetBool("IsSliding", true);
        }

        if (brain.Actions.Slide.WasReleasedThisFrame())
        {
            Animator.SetBool("IsSliding", false);
        }
    }

    public void UpdateThrowingAnimation(PlayerBrain brain)
    {
        if (brain.Actions.Throw.WasPressedThisFrame() && brain.Actions.Aim.IsPressed())
        {
            Animator.SetTrigger("Throw");
        }
    }

    public void UpdateAimingAnimation(PlayerBrain brain)
    {
        Animator.SetBool("IsAiming", brain.Actions.Aim.IsPressed());
        if(brain.Actions.Aim.IsPressed())
        {
            //lerp
            Animator.SetLayerWeight(1, 1);
        }
        else if(brain.Actions.Aim.WasReleasedThisFrame())
        {
            //lerp
            Animator.SetLayerWeight(1, 0);
        }
    }

    public void UpdateWalkingAnimation(bool isMoving)
    {
        Animator.SetBool("IsMoving",isMoving);
    }

    public void UpdateWalkingAnimationMagnitude(float inputMagnitude)
    {
        Animator.SetFloat("InputMagnitude", inputMagnitude, _dampTime, Time.deltaTime);
    }

    public void UpdateAnimationGroundedState(bool isGrounded)
    {
        Animator.SetBool("IsGrounded", isGrounded);
    }

    public void UpdateSwingAnimation(bool isSwinging)
    {
        Animator.SetBool("IsSwinging", isSwinging);
    }

    public void UpdateAnimationSpeed()
    {
        _animator.speed = Time.timeScale;
    }

    public void DisableBaseLocomotionAnimations()
    {
        Animator.SetBool("IsMoving", false);
        Animator.SetFloat("InputMagnitude", 0);
    }

}
