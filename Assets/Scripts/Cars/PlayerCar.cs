using UnityEngine;
using System.Collections;

public class PlayerCar : MonoBehaviour
{
    public GameObject flameParticles;
    public bool isCoroRunning;
    [SerializeField] private float speed = 1;
    private bool startMove;
    private float localSpeed;
    [SerializeField] private Rigidbody rb;

    private void Start()
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

        Events.Instance.LoseEvent.AddListener(() => speed = 0);

    }

    private void OnDisable()
    {
        StopAllCoroutines();
        Events.Instance.ZoneClickEvent -= IncreaseSpeed;
        rb = null;
        Events.Instance.StartGameEvent.RemoveListener(() => startMove = true);
        Events.Instance.WinEvent.RemoveListener(() => startMove = false);
        Events.Instance.BlueZoneClickEvent -= () => flameParticles.SetActive(true);
        Events.Instance.LoseEvent.RemoveListener(() => speed = 0);
        isCoroRunning = false;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            IncreaseSpeed();
        }
    }

    public void IncreaseSpeed()
    {
        isCoroRunning = true;
        if (isCoroRunning)
        {
            StartCoroutine(IncreaseSpeedNumerator());
            Debug.Log("Done");
        }
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
        localSpeed = speed;
        float newSpeed = localSpeed * 1.6f;
        while (localSpeed < newSpeed)
        {
            localSpeed += 20f;
            yield return new WaitForSeconds(.2f);
        }

        while (localSpeed > speed)
        {
            localSpeed -= 10f;
            yield return new WaitForSeconds(.08f);
        }
        isCoroRunning = false;
    }
}
