using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static CKB.Utilities.CommonGameplayFacade;

public class GameResultWindow : MonoBehaviour
{
    [SerializeField] private TotalRankingScore[] rankingScores;
    [SerializeField] private GameObject playWindow;
    [SerializeField] private GameObject rewardsWindow;
    [SerializeField] private Button continueBtn;

    [Header("Tween Animation")]
    [SerializeField] private Transform rankingIcon;
    [SerializeField] private Transform levelIcon;
    [SerializeField] private Transform leaderboard;

    [Header("Player Rank")]
    [SerializeField] private TextMeshProUGUI playerRankText;
    [SerializeField] private TextMeshProUGUI playerLevelText;


    private void OnEnable()
    {
        playWindow.SetActive(false);

        SetPlayerRanking();
        Rank.SetTotalRanking(rankingScores);

        leaderboard.localScale = new Vector3(1, 0, 1);

        rankingIcon.ToPunchScale();
        levelIcon.ToPunchScale();
        leaderboard.ToScaleY(0.4f);
    }

    private void SetPlayerRanking()
    {
        playerRankText.text = Rank.GetPlayerRank().ToString();
        playerLevelText.text = GroupPlayer.Level.ToString();
    }

    public void OnRewards()
    {
        rewardsWindow.SetActive(true);
        gameObject.SetActive(false);
    }
}
