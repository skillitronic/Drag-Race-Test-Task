using DG.Tweening;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

class DebugScript : MonoBehaviour
{
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
            SaveSystem.Save("levels", SaveData.Current.levelIndex);
            Debug.Log("Saved");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //load level

            SaveSystem.Load("levels", ref SaveData.Current.levelIndex);

            Debug.Log($"Level is now {SaveData.Current.levelIndex}");

        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //set win bool to true in current index

            GameController.Instance.levelList[SaveData.Current.levelIndex].isWon = true;
            Debug.Log($"isWon now {GameController.Instance.levelList[SaveData.Current.levelIndex].isWon}");
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //set chosen bool to true in current index

            GameController.Instance.levelList[SaveData.Current.levelIndex].isChosen = true;
            Debug.Log($"isChosen now {GameController.Instance.levelList[SaveData.Current.levelIndex].isChosen}");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //check states of current level

            Debug.Log($"{GameController.Instance.levelList[SaveData.Current.levelIndex].isWon} {GameController.Instance.levelList[SaveData.Current.levelIndex].isChosen}");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //check states of current level
            SceneController.RestartScene();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(Events.Instance.ZoneClickEvent.GetInvocationList().Length);
        }

    }
}
