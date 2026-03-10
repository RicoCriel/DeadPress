using UnityEngine;

public class PlayerLookAt : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private Camera _camera;
    [Range(0, 1f)]
    [SerializeField] private float _lookAtWeight;
    [Range(0, 1f)]
    [SerializeField] private float _bodyWeight;
    [Range(0, 1f)]
    [SerializeField] private float _headWeight;
    [Range(0, 1f)]
    [SerializeField] private float _eyesWeight;
    [Range(0, 1f)]
    [SerializeField] private float _clampWeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (_camera == null)
            return;

        _animator.SetLookAtWeight(_lookAtWeight, _bodyWeight, _headWeight, _eyesWeight, _clampWeight);
        Ray lookAtRay = new Ray(transform.position, _camera.transform.forward);
        _animator.SetLookAtPosition(lookAtRay.GetPoint(25f));
    }
}
