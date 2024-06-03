using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CKB.Core;
using static CKB.Utilities.CommonGameplayFacade;

public class GameReviveWindow : MonoBehaviour
{
    [SerializeField] private GameObject resultCanvas;
    [SerializeField] private Image countDownCircle;
    [SerializeField] private TextMeshProUGUI countDownText;
    private float countDown = 5f;

    private void Update()
    {
        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
            countDown = Mathf.Max(0, countDown);
            countDownText.text = Mathf.CeilToInt(countDown).ToString();
            countDownCircle.fillAmount = countDown * 0.2f;

            if (countDown <= 0)
            {
                ShowGameResult();
            }
        }
    }

    private void ShowGameResult()
    {
        gameObject.SetActive(false);
        StateMachine.Push(new GameOverState());
    }

    public void OnRevive()
    {
        GroupPlayer.Revive();
        gameObject.SetActive(false);
    }

    public void OnContinue()
    {
        ShowGameResult();
    }
}
