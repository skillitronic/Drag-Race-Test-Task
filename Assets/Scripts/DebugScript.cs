using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

class DebugScript : MonoBehaviour
{
    [SerializeField]public Collection<Level> referenceLevels;

    public SaveData data;
    private void Start()
    {
        data = SaveData.Current;
        data.levels = referenceLevels;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //increase level

            SaveData.Current.levelIndex++;
            Debug.Log($"Was {SaveData.Current.levelIndex - 1} Now {SaveData.Current.levelIndex}");

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //save level
            SaveSystem.Save("levels",SaveData.Current.levelIndex);
            Debug.Log("Saved");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //load level

            SaveData.Current.levelIndex = (int)SaveSystem.Load("levels");
            Debug.Log($"Level is now {SaveData.Current.levelIndex}");
            Debug.Log($"Win State {referenceLevels[SaveData.Current.levelIndex].isWon} Choose state {referenceLevels[SaveData.Current.levelIndex].isChosen}");
        }


        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Level item = ScriptableObject.CreateInstance<Level>();
            SaveData.Current.levels.Add(item);
            Debug.Log("Added Item to list");
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SaveSystem.Save("1levels",SaveData.Current.levels);
            Debug.Log("Savied Level List");
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SaveData.Current.levels = (Collection<Level>)SaveSystem.Load("1levels");
            Debug.Log($"Loaded list with {SaveData.Current.levels.Count} items");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //Check SaveData list
            Debug.Log($"Current level count is {SaveData.Current.levels.Count}");
        }

    }
}
