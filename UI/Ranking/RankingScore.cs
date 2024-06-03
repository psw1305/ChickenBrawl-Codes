using UnityEngine;
using TMPro;
using CKB;
using static CKB.Utilities.CommonGameplayFacade;

public class RankingScore : MonoBehaviour
{
    [SerializeField] private TMP_Text rank;
    [SerializeField] private TMP_Text nickname;
    [SerializeField] private TMP_Text score;

    public void SetScore(Group group, int ranking = 0)
    {
        SetPlayerRank(ranking);
        SetColor(group);
        nickname.text = group.Nickname;
        score.text = group.Level.ToString();
    }

    private void SetPlayerRank(int ranking)
    {
        if (ranking == 0) return;

        rank.text = $"{ranking}th";
    }

    private void SetColor(Group group)
    {
        if (group == GroupPlayer)
        {
            rank.color = GameData.PlayerRankColor;
        }
        else
        {
            rank.color = Color.white;
        }
    }

}
