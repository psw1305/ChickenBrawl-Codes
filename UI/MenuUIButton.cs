using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using static CKB.Utilities.CommonGameplayFacade;

public class MenuUIButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI priceText;
    private Sequence punchSequence;

    public Button Button => button;
    public TextMeshProUGUI LevelText => levelText;
    public TextMeshProUGUI PriceText => priceText;

    private void OnEnable()
    {
        Button.onClick.AddListener(PunchAnimation);
    }

    public void CheckEnoughMoney(bool isEnough)
    {
        if (!isEnough)
        {
            button.interactable = false;
        }
    }

    private void PunchAnimation()
    {
        if (punchSequence != null && punchSequence.IsActive())
        {
            punchSequence.Kill();
            transform.localScale = Vector3.one;
        }

        punchSequence = DOTween.Sequence()
            .Append(transform.DOPunchScale(Vector3.one * 0.2f, 0.4f, 1, 1));
    }
}
