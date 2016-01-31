using Prime31;
using System.Collections;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    public float smoothDampTime = 0.2f;
    public float smoothAngle = 1.0f;

    [HideInInspector]
    public new Transform transform;
    public Vector3 cameraOffset;
    public bool useFixedUpdate = false;

    public Vector3 Pivot
    {
        get
        {
            return target.position - cameraOffset;
        }
    }

    private CharacterController2D _playerController;
    private Vector3 _smoothDampVelocity;

    private void Awake()
    {
        transform = gameObject.transform;
        _playerController = target.GetComponent<CharacterController2D>();
    }

    private void LateUpdate()
    {
        if (!useFixedUpdate)
            updateCameraPosition();
    }

    private void FixedUpdate()
    {
        if (useFixedUpdate)
            updateCameraPosition();
    }

    private void updateCameraPosition()
    {
        float smoothDampTimeOriginal = smoothDampTime;
        if (transform.rotation.eulerAngles != Vector3.zero)
        {
            //Debug.Log(smoothDampTime);
            //smoothDampTime = 0.1f;
        }

        if (_playerController == null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position - cameraOffset, ref _smoothDampVelocity, smoothDampTime);
            return;
        }

        if (_playerController.velocity.x > 0)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position - cameraOffset, ref _smoothDampVelocity, smoothDampTime);
        }
        else
        {
            var leftOffset = cameraOffset;
            leftOffset.x *= -1;
            transform.position = Vector3.SmoothDamp(transform.position, target.position - leftOffset, ref _smoothDampVelocity, smoothDampTime);
        }

        smoothDampTime = smoothDampTimeOriginal;

        if (transform.rotation.eulerAngles != Vector3.zero)
        {
            float angle = Vector3.Angle(transform.up, Vector3.up);
            Vector3 cross = Vector3.Cross(transform.up, Vector3.up);
            var sign = -1.0f;
            if (cross.z > 0)
                sign = 1.0f;
            if (angle < smoothAngle * Time.deltaTime)
            {
                var rotation = Camera.main.transform.rotation;
                Camera.main.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
            else
            {
                Camera.main.transform.Rotate(new Vector3(0.0f, 0.0f, sign * smoothAngle * Time.deltaTime));
            }
        }
    }
}