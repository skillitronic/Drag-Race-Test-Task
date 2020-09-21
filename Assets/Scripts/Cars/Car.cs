using UnityEngine;
public abstract class Car : MonoBehaviour
{
    [SerializeField] protected float speed = 1;
    [SerializeField] protected bool startMove;
    protected float localSpeed;
    protected Rigidbody rb;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        localSpeed = speed;
        Events.Instance.StartGameEvent.AddListener(() => startMove = true);
        Events.Instance.WinEvent.AddListener(() => startMove = false);
    }

    private void OnDisable()
    {
        Events.Instance.StartGameEvent.RemoveListener(() => startMove = true);
        Events.Instance.WinEvent.RemoveListener(() => startMove = false);
    }

    private void FixedUpdate()
    {
        if (startMove)
        {
            rb.velocity = -transform.forward * localSpeed * Time.fixedDeltaTime;
        } else
        {
            if (rb.velocity.z > 0)
            {
                rb.velocity -= Vector3.forward * Time.fixedDeltaTime;
            }
        }

        if (rb.velocity.z < 0)
        {
            rb.velocity = Vector3.zero;
        }
    }
}
