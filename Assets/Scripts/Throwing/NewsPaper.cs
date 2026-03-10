using UnityEngine;

public class NewsPaper : MonoBehaviour
{
    [Range(0,1000f)]
    [SerializeField] private float _force;
    private Rigidbody _rb;
    private Camera _camera;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _rb.AddForce(_camera.transform.forward *  _force, ForceMode.Impulse);
    }
}
