using UnityEngine;
using System.Collections;

public class PlayerCar : Car
{
    public GameObject flameParticles;
    private bool isCoroRunning;
    //List of Upgrades

    private void Awake()
    {
        CameraScript.Instance.playerCar = transform;
    }
    public void Start()
    {
        Events.Instance.ZoneClickEvent += IncreaseSpeed;
        Events.Instance.BlueZoneClickEvent += () => flameParticles.SetActive(true);
        Events.Instance.LoseEvent.AddListener(() => speed = 0);
    }

    public void OnDisable()
    {
        Events.Instance.ZoneClickEvent -= IncreaseSpeed;
        Events.Instance.BlueZoneClickEvent -= () => flameParticles.SetActive(true);
        Events.Instance.LoseEvent.RemoveListener(() => speed = 0);
    }

    public void IncreaseSpeed()
    {
        isCoroRunning = true;
        if (isCoroRunning)
        {
            StartCoroutine(nameof(IncreaseSpeedNumerator));
        }
    }

    private IEnumerator IncreaseSpeedNumerator()
    {
        float oldSpeed = speed;
        float newSpeed = speed * 1.6f;
        while (speed < newSpeed)
        {
            speed += 20f;
            yield return new WaitForSeconds(.2f);
        }

        while (speed > oldSpeed)
        {
            speed -= 10f;
            yield return new WaitForSeconds(.08f);
        }
        isCoroRunning = false;
    }
}
