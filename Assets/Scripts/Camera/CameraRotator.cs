using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraRotator : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float orbitDegreesPerSec = 180.0f;
    public bool isAutoRotating = true;
    public bool isDragging;
    [SerializeField] private float rotSpeed;

    public void Update()
    {
        if (target != null && isAutoRotating)
        {
            //target.RotateAround(transform.position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);
            target.Rotate(new Vector3(0, orbitDegreesPerSec * Time.deltaTime, 0));
        }

        if (Input.GetMouseButton(0))
        {
            isDragging = true;
            isAutoRotating = false;
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            target.Rotate(Vector3.up, -rotX);
        }
        else
        {
            isDragging = false;
        }
    }
}
