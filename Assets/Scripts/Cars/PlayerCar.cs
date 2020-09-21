using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

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
