using UnityEngine;
[CreateAssetMenu(fileName = "Level", menuName = "CreateLevel"),System.Serializable]
public class Level : ScriptableObject
{
    public GameObject levelReference;
    public bool isWon;
    public bool isChosen;

}