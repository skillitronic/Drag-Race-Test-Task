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
        Events.Instance.UpgradeButtonEvent += EquipUpgrade;

        for (int i = 0; i < upgradeImages.Length; i++)
        {
            upgradeImages[i].sprite = GameController.Instance.levelList[SaveData.Current.levelIndex].upgrades[i].image;
            upgradeCostTexts[i].SetText(GameController.Instance.levelList[SaveData.Current.levelIndex].upgrades[i].cost.ToString());
            upgradeButtons[i].onClick.AddListener(Events.Instance.UpgradeButtonEvent.Invoke);
        }
    }

    private void OnDisable()
    {
        Events.Instance.UpgradeButtonEvent -= EquipUpgrade;
    }

    private void EquipUpgrade()
    {
        if (GameController.Instance.score >= GameController.Instance.levelList[SaveData.Current.levelIndex].upgrades[transform.GetSiblingIndex()].cost)
        {
            GameController.Instance.equipedUpgrades.upgradeList.Add(GameController.Instance.levelList[SaveData.Current.levelIndex].upgrades[transform.GetSiblingIndex()]);
            GameController.Instance.score = 0;
            GameController.Instance.levelList[SaveData.Current.levelIndex].isChosen = true;
            SaveData.Current.levelIndex += 1;
            SaveSystem.Save("levels", SaveData.Current.levelIndex);
            GameController.Instance.CarCheck();
            GameController.Instance.SpawnLevel();
            SceneController.UnloadSceneByName("UpgradeScene");
        }
    }
}
