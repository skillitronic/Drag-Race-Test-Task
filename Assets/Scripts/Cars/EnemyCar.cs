
using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    private bool startMove;
    [SerializeField] private Rigidbody rb;


    private void OnEnable()
    {
        Events.Instance.StartGameEvent.AddListener(() => startMove = true);
        Events.Instance.WinEvent.AddListener(() => startMove = false);
    }

    public void OnDisable()
    {
        Events.Instance.StartGameEvent.RemoveListener(() => startMove = true);
        Events.Instance.WinEvent.RemoveListener(() => startMove = false);
    }

    private void FixedUpdate()
    {
        if (startMove)
        {
            rb.velocity = -transform.forward * speed * Time.fixedDeltaTime;
        }
        else
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
