using Unity.Cinemachine;
using UnityEngine;

public class ThirdpersonCameraController : MonoBehaviour
{
    [Header("Player Input")]
    [SerializeField] private PlayerInputActions _playerInputData;

    [Header("Camera")]
    [SerializeField] private CinemachineCamera _camera;

    [Header("Sensitivity")]
    [Range(0f, 100f)]
    [SerializeField] private float _sensitivity = 20f;

    [Header("Clamping")]
    [SerializeField] private float _topClamp = 85f;
    [SerializeField] private float _bottomClamp = -40f;

    [Header("Follow Target")]
    [SerializeField] private Transform _target;

    [Header("Input Filtering")]
    [Range(0f, 0.5f)]
    [SerializeField] private float _deadzone = 0.12f;   // gamepad drift fix
    [Range(0f, 30f)]
    [SerializeField] private float _smoothing = 12f;    // feel: higher = snappier

    private Vector2 _lookRaw;
    private Vector2 _lookSmoothed;

    private float _pitch;
    private float _yaw;

    private void Awake()
    {
        if (_camera == null)
            _camera = GetComponent<CinemachineCamera>();
    }

    private void Update()
    {
        _lookRaw = _playerInputData.Look.ReadValue<Vector2>();

        // Deadzone (important for sticks)
        if (_lookRaw.sqrMagnitude < _deadzone * _deadzone)
            _lookRaw = Vector2.zero;

        // Smooth input for nicer camera feel
        _lookSmoothed = Vector2.Lerp(_lookSmoothed, _lookRaw, 1f - Mathf.Exp(-_smoothing * Time.deltaTime));
    }

    private void LateUpdate()
    {
        Rotate(_lookSmoothed);
    }

    private void Rotate(Vector2 look)
    {
        if (_target == null) return;

        _yaw += look.x * _sensitivity * Time.deltaTime;
        _pitch -= look.y * _sensitivity * Time.deltaTime;

        _pitch = Mathf.Clamp(_pitch, _bottomClamp, _topClamp);

        _target.rotation = Quaternion.Euler(_pitch, _yaw, 0f);
    }
}
