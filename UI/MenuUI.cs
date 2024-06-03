using UnityEngine;
using UnityEngine.UI;
using CKB.Core;
using CKB.Utilities;
using static CKB.Utilities.CommonGameplayFacade;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button playingBtn;
    [SerializeField] private MenuUIButton startLevelIncreaseButton;
    [SerializeField] private MenuUIButton speedUpIncreaseButton;
    [SerializeField] private MenuUIButton itemUpgradeIncreaseButton;

    private void Start()
    {
        playingBtn.onClick.AddListener(() => StateMachine.Push(new PlayingState()));

        startLevelIncreaseButton.Button.onClick.AddListener(IncreaseStartLevel);
        speedUpIncreaseButton.Button.onClick.AddListener(IncreaseSpeedUp);
        itemUpgradeIncreaseButton.Button.onClick.AddListener(IncreaseItemUpgrade);

        UpdateStats();
    }

    private void IncreaseStartLevel()
    {
        DB.StartIncreaseLevel.Value++;
        DB.Money.Value -= GameData.StartLevelUpgradePrice;
        
        GroupPlayer.LevelUp();

        UpdateStats();
    }
    
    private void IncreaseSpeedUp()
    {
        DB.SpeedUpIncreaseLevel.Value++;
        DB.Money.Value -= GameData.SpeedUpUpgradePrice;

        GroupPlayer.SpeedUp();

        UpdateStats();
    }

    private void IncreaseItemUpgrade()
    {
        DB.ItemUpgradeIncreaseLevel.Value++;
        DB.Money.Value -= GameData.ItemUpgradePrice;

        GroupPlayer.ItemUp();

        UpdateStats();
    }

    /// <summary>
    /// 현 재화로 강화가 가능한지 체크하는 메서드
    /// 가능하면 버튼 활성화, 아니면 비활성화
    /// </summary>
    private void CheckForDeactivatingButtons()
    {
        startLevelIncreaseButton.CheckEnoughMoney(DB.Money.Value >= GameData.StartLevelNextUpgradePrice);
        speedUpIncreaseButton.CheckEnoughMoney(DB.Money.Value >= GameData.SpeedUpNextUpgradePrice);
        itemUpgradeIncreaseButton.CheckEnoughMoney(DB.Money.Value >= GameData.ItemNextUpgradePrice);

        if (DB.StartIncreaseLevel.Value >= GameData.MaxLevelUpgrade)
        {
            startLevelIncreaseButton.Button.interactable = false;
            startLevelIncreaseButton.PriceText.text = "MAX";
        }

        if (DB.SpeedUpIncreaseLevel.Value >= GameData.MaxLevelUpgrade)
        {
            speedUpIncreaseButton.Button.interactable = false;
            speedUpIncreaseButton.PriceText.text = "MAX";
        }

        if (DB.ItemUpgradeIncreaseLevel.Value >= GameData.MaxLevelUpgrade)
        {
            itemUpgradeIncreaseButton.Button.interactable = false;
            itemUpgradeIncreaseButton.PriceText.text = "MAX";
        }
    }

    /// <summary>
    /// 플레이어 스탯 업데이트
    /// </summary>
    private void UpdateStats()
    {
        startLevelIncreaseButton.LevelText.text = $"Lv +{DB.StartIncreaseLevel.Value}";
        speedUpIncreaseButton.LevelText.text = $"Lv +{DB.SpeedUpIncreaseLevel.Value}";
        itemUpgradeIncreaseButton.LevelText.text = $"Lv +{DB.ItemUpgradeIncreaseLevel.Value}";

        startLevelIncreaseButton.PriceText.text = CMath.ConvertPostfix(GameData.StartLevelNextUpgradePrice);
        speedUpIncreaseButton.PriceText.text = CMath.ConvertPostfix(GameData.SpeedUpNextUpgradePrice);
        itemUpgradeIncreaseButton.PriceText.text = CMath.ConvertPostfix(GameData.ItemNextUpgradePrice);

        CheckForDeactivatingButtons();
    }
}
