using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private Transform target = null;
    [SerializeField] private float orbitDegreesPerSec = 60.0f;
    [SerializeField] private float rotSpeed = 0;

    private bool isAutoRotating = true;

    public void Update()
    {
        if (target != null && isAutoRotating)
        {
            //target.RotateAround(transform.position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);
            target.Rotate(new Vector3(0, orbitDegreesPerSec * Time.deltaTime, 0));
        }

        if (Input.GetMouseButton(0))
        {
            isAutoRotating = false;
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            target.Rotate(Vector3.up, -rotX);
        }
    }
}
