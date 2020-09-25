using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Car Container", menuName = "Create Car Container")]
public class CarContainer : ScriptableObject
{
    public int carIndex;
    public GameObject[] cars;
}
