using UnityEngine;

public class PlayerBrain : MonoBehaviour
{
    [SerializeField] private PlayerInputActions _actions;
    [SerializeField] private PlayerLocomotion _locomotion;

    public PlayerLocomotion Locomotion => _locomotion;
    public PlayerInputActions Actions => _actions;
}
