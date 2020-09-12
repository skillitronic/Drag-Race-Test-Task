using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameController : Singleton<MonoBehaviour>
{
    private void Start()
    {
/*        SaveData.Current.levelIndex = (int)SaveSystem.Load("levelIndex");

        if (SaveData.Current.levels[SaveData.Current.levelIndex].isWon == true && SaveData.Current.levels[SaveData.Current.levelIndex].isChosen == false)
        {
            SceneManager.LoadSceneAsync("UpgradeScene", LoadSceneMode.Additive);
        }*/
    }
}
