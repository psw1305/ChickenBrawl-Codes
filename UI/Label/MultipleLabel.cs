using UnityEngine;
using TMPro;

public class MultipleLabel : MonoBehaviour
{
    [SerializeField] private TMP_Text multipleText;

    public void IsActive(bool check)
    {
        if (check)
        {
            multipleText.color = Color.yellow;
        }
        else
        {
            multipleText.color = Color.white;
        }
    }
}
