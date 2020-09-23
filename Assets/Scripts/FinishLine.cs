using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField]protected bool hasCrossed = false;

    private void Awake()
    {
        hasCrossed = false;
        Debug.Log("OnAwakeCheck");
    }

    private void OnEnable()
    {
        //hasCrossed = false;
    }

    private void Start()
    {
        GameController.Instance.finishLineReference = transform;
        //hasCrossed = false;
    }

    private void OnDestroy()
    {
        hasCrossed = false;
        Debug.Log("OnDestroyCheck");
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Check");
        if (hasCrossed == false)
        {
            Debug.Log("Re");
            if (other.gameObject.GetComponent<PlayerCar>())
            {
                Events.Instance.WinEvent.Invoke();
            }
            else if (other.gameObject.GetComponent<EnemyCar>())
            {
                Events.Instance.LoseEvent.Invoke();
            }
            hasCrossed = true;
        }
        
    }
}
