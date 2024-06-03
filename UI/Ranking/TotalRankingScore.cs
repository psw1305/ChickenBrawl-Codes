using UnityEngine;
using TMPro;
using CKB.Utilities;

public class TotalRankingScore : MonoBehaviour
{
    [SerializeField] private GameObject frame;
    [SerializeField] private TMP_Text rank;
    [SerializeField] private TMP_Text nickname;
    [SerializeField] private TMP_Text rewards;

    public void Init(int index, string nickname, int level)
    {
        rank.text = SetRank(index);
        this.nickname.text = nickname;
        rewards.text = SetRewards(level);
    }

    public void SetPlayerFrame()
    {
        frame.SetActive(true);
    }

    private string SetRank(int index)
    {
        return index switch
        {
            0 => "1st",
            1 => "2nd",
            2 => "3rd",
            _ => $"{index + 1}th",
        };
    }

    private string SetRewards(int level)
    {
        return CMath.ConvertPostfix(level * 5f);
    }
}
