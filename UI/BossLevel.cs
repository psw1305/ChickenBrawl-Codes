using UnityEngine;
using TMPro;
using static CKB.Utilities.CommonGameplayFacade;

public class BossLevel : MonoBehaviour
{
    [SerializeField] private TMP_Text bossLevelLabel;

    public void DisplayLevel()
    {
        bossLevelLabel.text = GameData.BossLevel.ToString();
    }
}
