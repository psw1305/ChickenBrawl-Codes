using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CKB.Utilities;
using static CKB.Utilities.CommonGameplayFacade;

public class BoostUI : MonoBehaviour
{
    [SerializeField] private Button magnetBtn;
    [SerializeField] private Button speedBtn;
    [SerializeField] private Button levelBtn;
    [SerializeField] private Button coinsBtn;
    [SerializeField] private TMP_Text coinsText;
    private float coinRewards;

    private void Awake()
    {
        magnetBtn.onClick.AddListener(ActiveBoostMagnet);
        speedBtn.onClick.AddListener(ActiveBoostSpeed);
        levelBtn.onClick.AddListener(ActiveBoostLevel);
        coinsBtn.onClick.AddListener(ActiveBoostCoins);

        coinRewards = GameData.BoostCoinRewards();
        coinsText.text = $"+ {CMath.ConvertPostfix(coinRewards)}";
    }

    private void ActiveBoostMagnet()
    {
        GroupPlayer.MagnetBoost();
        magnetBtn.gameObject.SetActive(false);
    }

    private void ActiveBoostSpeed()
    {
        GroupPlayer.SpeedBoost();
        speedBtn.gameObject.SetActive(false);
    }

    private void ActiveBoostLevel()
    {
        GroupPlayer.LevelBoost();
        levelBtn.gameObject.SetActive(false);
    }

    private void ActiveBoostCoins()
    {
        GroupPlayer.CoinsBoost(coinRewards);
        coinsBtn.gameObject.SetActive(false);
    }
}
