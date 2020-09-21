using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private bool hasCrossed;

    private void OnDisable()
    {
        hasCrossed = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (hasCrossed == false)
        {
            if (other.gameObject.GetComponent<PlayerCar>())
            {
                Events.Instance.WinEvent.Invoke();
            }
            else
            {
                Events.Instance.LoseEvent.Invoke();
            }
            hasCrossed = true;
        }
        
    }
}
