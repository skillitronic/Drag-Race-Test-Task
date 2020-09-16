using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    public GameObject flameParticles;
    //List of Upgrades
    private void Awake()
    {
        CameraScript.Instance.playerCar = this.transform;

    }
    public void OnEnable()
    {
        Events.Instance.ZoneClickEvent += () => flameParticles.SetActive(true);
    }

    public void OnDisable()
    {
        Events.Instance.ZoneClickEvent -= () => flameParticles.SetActive(true);
    }
}
