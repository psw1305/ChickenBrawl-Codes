using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CKB.Core;
using CKB.Utilities;
using static CKB.Utilities.CommonGameplayFacade;
using DG.Tweening;

public class RewardsWindow : MonoBehaviour
{
    [Header("Currency")]
    [SerializeField] private GameObject currencyGem;
    [SerializeField] private GameObject currencyCoin;

    [Header("Rewards")]
    [SerializeField] private Transform rewardsTitle;
    [SerializeField] private Transform rewardsIcon;
    [SerializeField] private Transform rewards;
    [SerializeField] private TMP_Text rewardsText;

    [Header("Roulette")]
    [SerializeField] private MultipleLabel[] multipleLabels;
    [SerializeField] private GameObject roulette;
    [SerializeField] private Transform roulettePointer;
    [SerializeField] private TMP_Text multipleRewardsText;
    [SerializeField] private Button multipleBtn;
    [SerializeField] private Button continueBtn;

    private float moneyRewards;
    private int moneyMultiple;
    private Tween tween;

    private void OnEnable()
    {
        moneyRewards = GroupPlayer.Level * 5f;
        rewardsText.text = $"+{CMath.ConvertPostfix(moneyRewards)}";

        //currencyGem.SetActive(true);
        currencyCoin.SetActive(true);

        rewardsTitle.transform.localScale = new Vector3(1, 0, 1);
        rewardsIcon.localScale = new Vector3(1, 0, 1);
        rewards.localScale = new Vector3(1, 0, 1);

        rewardsTitle.transform.ToScaleY();
        rewardsIcon.ToScaleY();
        rewards.ToScaleY();

        StartCoroutine(StartDelayCoroutine());
    }

    private void Update()
    {
        float pointerX = roulettePointer.localPosition.x;

        if (pointerX >= -250 && pointerX < -150)
        {
            MultipleLabelActive(0);
            moneyMultiple = 2;
        }
        else if (pointerX >= -150 && pointerX < -50)
        {
            MultipleLabelActive(1);
            moneyMultiple = 3;
        }
        else if (pointerX >= -50 && pointerX < 50)
        {
            MultipleLabelActive(2);
            moneyMultiple = 4;
        }
        else if (pointerX >= 50 && pointerX < 150)
        {
            MultipleLabelActive(3);
            moneyMultiple = 3;
        }
        else
        {
            MultipleLabelActive(4);
            moneyMultiple = 2;
        }

        multipleRewardsText.text = $"{CMath.ConvertPostfix(moneyRewards * moneyMultiple)}";
    }

    private void MultipleLabelActive(int index)
    {
        for (int i = 0; i < multipleLabels.Length; i++)
        {
            if (index == i)
            {
                multipleLabels[i].IsActive(true);
            }
            else
            { 
                multipleLabels[i].IsActive(false);
            }
        }
    }

    public void MultipleRewards()
    {
        StartCoroutine(FlyingUICoroutine(moneyRewards * moneyMultiple));
    }

    public void ReloadScene()
    {
        StartCoroutine(FlyingUICoroutine(moneyRewards));
    }

    #region Coroutine

    private IEnumerator StartDelayCoroutine()
    {
        yield return new WaitForSeconds(1f);

        roulette.SetActive(true);
        TweenShifting();

        yield return new WaitForSeconds(1f);

        continueBtn.gameObject.SetActive(true);
    }

    private IEnumerator FlyingUICoroutine(float rewards)
    {
        tween.Kill();

        multipleBtn.interactable = false;
        continueBtn.interactable = false;

        DB.Money.Value += rewards;

        yield return new WaitForSeconds(1f);

        var loader = this.FindLazy<SceneLoader>();
        loader.ReloadCurrentScene();
    }

    #endregion

    public void TweenShifting()
    {
        tween = roulettePointer
            .DOLocalMoveX(500, 500)
            .SetEase(Ease.Linear)
            .SetRelative()
            .SetSpeedBased()
            .SetLoops(-1, LoopType.Yoyo);
    }
}
