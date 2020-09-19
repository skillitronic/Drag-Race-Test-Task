using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    public GameObject flameParticles;
    //List of Upgrades

    private void Awake()
    {
        CameraScript.Instance.playerCar = transform;

    }
    public void OnEnable()
    {
        Events.Instance.BlueZoneClickEvent += () => flameParticles.SetActive(true);
    }

    public void OnDisable()
    {
        Events.Instance.BlueZoneClickEvent -= () => flameParticles.SetActive(true);
    }
}
