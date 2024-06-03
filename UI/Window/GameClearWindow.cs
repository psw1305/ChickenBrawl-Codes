using UnityEngine;
using UnityEngine.UI;
using static CKB.Utilities.CommonGameplayFacade;

public class GameClearWindow : MonoBehaviour
{
    [Header("Window")]
    [SerializeField] private GameObject playWindow;
    [SerializeField] private GameObject indicator;
    [SerializeField] private Image blind;

    [Header("Tween UI")]
    [SerializeField] private Transform timeOver;
    [SerializeField] private Transform bossFight;

    private void OnEnable()
    {
        playWindow.SetActive(false);
        indicator.SetActive(false);
        TweenAnimation.ToTimeEnd(timeOver, blind, EnableBossFight);
    }

    private void EnableBossFight()
    {
        BossBattle.Init();
        TweenAnimation.ToBossFight(bossFight, blind, BossBattle.BossBattleStart);
    }
}
