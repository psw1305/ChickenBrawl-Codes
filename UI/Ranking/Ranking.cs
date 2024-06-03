using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CKB;
using static CKB.Utilities.CommonGameplayFacade;

public class Ranking : MonoBehaviour
{
    public List<Group> Groups = new();
    private List<Group> ranks = new();

    [SerializeField] private RankingScore[] scores;
    [SerializeField] private ObjectIndicator indicator;

    public int GetPlayerRank()
    {
        var playerGroup = ranks.FirstOrDefault(group => group.Nickname == GroupPlayer.Nickname);
        var playerRank = ranks.IndexOf(playerGroup);
        return playerRank + 1;
    }

    public void UpdateScore()
    {
        ranks = Groups.OrderByDescending(group => group.Level).ToList();

        VisualFirstRanking();

        for (int i = 0; i < scores.Length - 1; i++)
        {
            scores[i].SetScore(ranks[i]);
        }

        int playerRank = GetPlayerRank();

        if (playerRank >= 4)
        {
            scores[3].gameObject.SetActive(true);
            scores[3].SetScore(ranks[playerRank - 1], playerRank);
        }
        else
        {
            scores[3].gameObject.SetActive(false);
        }
    }

    private void VisualFirstRanking()
    {
        if (indicator.Target != null)
        {
            var prevTarget = indicator.Target.GetComponent<Unit>().Group;
            prevTarget.SetCrownActive(false);
        }

        indicator.Target = ranks[0].GetUnit();
        ranks[0].SetCrownActive(true);
    }

    public void RemoveRankingGroup(Group group)
    {
        Groups.Remove(group);
    }

    public void SetTotalRanking(TotalRankingScore[] rankingScores)
    {
        int playerRank = GetPlayerRank();

        for (int i = 0; i < rankingScores.Length - 1; i++)
        {
            rankingScores[i].Init(i, ranks[i].Nickname, ranks[i].Level);

            if (playerRank - 1 == i)
            {
                rankingScores[i].SetPlayerFrame();
            }
        }

        if (playerRank >= 7)
        {
            rankingScores[6].gameObject.SetActive(true);
            rankingScores[6].Init(playerRank, ranks[playerRank - 1].Nickname, ranks[playerRank - 1].Level);
            rankingScores[6].SetPlayerFrame();
        }
    }
}
