using UnityEngine;
[CreateAssetMenu(fileName = "Level", menuName = "CreateLevel")]
public class Level : ScriptableObject
{
    public GameObject levelReference;
    public bool isWon;
    public bool isChosen;

}