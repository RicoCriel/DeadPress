using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Player/Locomotion", order = 0)]
public class PlayerLocomotion : ScriptableObject
{
    public float RotationSpeed => _rotationSpeed;
    public float MaxSpeed => _maxSpeed;
    //public float JumpHeight => _jumpHeight;
    public float GroundDistance => _groundDistance;
    //public float AirAcceleration => _airAcceleration;
    //public float AirControl => _airControl;
    //public float AirDrag => _airDrag;
    //public float MaxAirSpeed => _maxAirSpeed;
    //public float DashDistance => _dashDistance;
    public LayerMask Ground => _groundLayer;

    [Range(0, 720f)]
    [SerializeField] private float _rotationSpeed;
    //[Range(0,10f)]
    //[SerializeField] private float _jumpHeight;
    //[Range(0,10f)]
    //[SerializeField] private float _dashDistance;
    [Range(0, 50f)]
    [SerializeField] private float _maxSpeed;
    //[Header("Airborne Settings")]
    //[SerializeField] private float _airControl = 2f;        // How much control player has in air
    //[SerializeField] private float _airDrag = 0.5f;         // How quickly horizontal speed decays with no input
    //[SerializeField] private float _airAcceleration = 7f;
    //[SerializeField] private float _maxAirSpeed = 8f;       // Max speed while airborne
    [Range(0, 1f)]
    [SerializeField] private float _groundDistance;
    [SerializeField] private LayerMask _groundLayer;

    [Header("Jump Settings")]
    public float JumpHeight = 2f;
    public float JumpApexTime = 0.5f;
    public float JumpGravityMultiplier = 0.5f;
    public float CoyoteTime = 0.15f;
    public float JumpBufferTime = 0.1f;

    [Header("Movement Settings")]
    public float Acceleration = 10f;
    public float GroundFriction = 5f;
    public float AirAcceleration = 3f;
    public float AirDrag = 0.5f;
    public float MaxAirSpeed = 10f;
}
