using UnityEngine;
using System.Collections;

public class PlayerCar : MonoBehaviour
{
    public GameObject flameParticles;
    public bool isCoroRunning;
    [SerializeField] private float speed = 1;
    private bool startMove;
    public float localSpeed;
    [SerializeField] private Rigidbody rb;

    private void Awake()
    {
        Events.Instance.ZoneClickEvent += IncreaseSpeed;
        Events.Instance.BlueZoneClickEvent += () => flameParticles.SetActive(true);
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        localSpeed = speed;
        Events.Instance.StartGameEvent.AddListener(() => startMove = true);
        Events.Instance.WinEvent.AddListener(() => startMove = false);
        Events.Instance.LoseEvent.AddListener(() => startMove = false);

    }

    private void OnDisable()
    {
        StopAllCoroutines();
        Events.Instance.ZoneClickEvent -= IncreaseSpeed;
        rb = null;
        Events.Instance.StartGameEvent.RemoveListener(() => startMove = true);
        Events.Instance.WinEvent.RemoveListener(() => startMove = false);
        Events.Instance.BlueZoneClickEvent -= () => flameParticles.SetActive(true);
        Events.Instance.LoseEvent.RemoveListener(() => startMove = false);
        isCoroRunning = false;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void IncreaseSpeed()
    {
        if (!isCoroRunning)
        {
            StartCoroutine("IncreaseSpeedNumerator");
            Debug.Log("Done");
        }
        isCoroRunning = true;
    }

    private void FixedUpdate()
    {
        if (startMove)
        {
            rb.velocity = -transform.forward * localSpeed * Time.fixedDeltaTime;
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

    public IEnumerator IncreaseSpeedNumerator()
    {
        float newSpeed = localSpeed * 1.5f;
        while (localSpeed < newSpeed)
        {
            localSpeed += 20f;
            yield return new WaitForSeconds(.2f);
        }
        isCoroRunning = false;
    }
}
