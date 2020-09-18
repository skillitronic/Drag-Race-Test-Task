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
        Events.Instance.UpgradeButtonEvent += () => GameController.Instance.score = 0;
        Events.Instance.UpgradeButtonEvent += () => GameController.Instance.levelList[SaveData.Current.levelIndex].isChosen = true;
        Events.Instance.UpgradeButtonEvent += () => SaveData.Current.levelIndex += 1;
        Events.Instance.UpgradeButtonEvent += () => SaveSystem.Save("levels", SaveData.Current.levelIndex);
        Events.Instance.UpgradeButtonEvent += () => GameController.Instance.SpawnLevel();
        Events.Instance.UpgradeButtonEvent += () => SceneController.UnloadSceneByName("UpgradeScene");

        for (int i = 0; i < upgradeImages.Length; i++)
        {
            upgradeImages[i].sprite = GameController.Instance.levelList[SaveData.Current.levelIndex].upgrades[i].image;
            upgradeCostTexts[i].SetText(GameController.Instance.levelList[SaveData.Current.levelIndex].upgrades[i].cost.ToString());
            upgradeButtons[i].onClick.AddListener(EquipUpgrade);
        }
    }

    private void OnDisable()
    {
        Events.Instance.UpgradeButtonEvent -= () => GameController.Instance.score = 0;
        Events.Instance.UpgradeButtonEvent -= () => GameController.Instance.levelList[SaveData.Current.levelIndex].isChosen = true;
        Events.Instance.UpgradeButtonEvent -= () => SaveData.Current.levelIndex += 1;
        Events.Instance.UpgradeButtonEvent -= () => SaveSystem.Save("levels", SaveData.Current.levelIndex);
        Events.Instance.UpgradeButtonEvent -= () => GameController.Instance.SpawnLevel();
        Events.Instance.UpgradeButtonEvent -= () => SceneController.UnloadSceneByName("UpgradeScene");
    }

    private void EquipUpgrade()
    {
        if (GameController.Instance.score >= GameController.Instance.levelList[SaveData.Current.levelIndex].upgrades[transform.GetSiblingIndex()].cost)
        {
            Events.Instance.UpgradeButtonEvent?.Invoke();
        }
    }
}
