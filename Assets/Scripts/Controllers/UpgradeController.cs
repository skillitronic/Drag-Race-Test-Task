using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    public Button[] upgradeButtons;
    public Image[] upgradeImages;
    public TextMeshProUGUI[] upgradeCostTexts;

    private void OnEnable()
    {
        for (int i = 0; i < upgradeImages.Length; i++)
        {
            upgradeImages[i].sprite = GameController.Instance.levelList[SaveData.Current.levelIndex].upgrades[i].image;
            upgradeCostTexts[i].SetText(GameController.Instance.levelList[SaveData.Current.levelIndex].upgrades[i].cost.ToString());
            upgradeButtons[i].onClick.AddListener(EquipUpgrade);
        }
    }

    private void EquipUpgrade()
    {
        if (GameController.Instance.score >= GameController.Instance.levelList[SaveData.Current.levelIndex].upgrades[transform.GetSiblingIndex()].cost)
        {
            Debug.Log(GameController.Instance.levelList[SaveData.Current.levelIndex].upgrades[transform.GetSiblingIndex()].cost);
            //GameController.Instance.score = 1000;
            //GameController.Instance.levelList[SaveData.Current.levelIndex].isChosen = true;
            //SaveData.Current.levelIndex += 1;
            // need to move somewhere else
            //SaveSystem.Save("levels", SaveData.Current.levelIndex);
            //SceneController.UnloadSceneByName("UpgradeScene");
        }
    }
}
