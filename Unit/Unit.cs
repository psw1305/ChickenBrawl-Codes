using UnityEngine;
using TMPro;

namespace CKB
{
    public class Unit : MonoBehaviour
    {
        public const int MAX_GROW = 5;

        public int GrowStack { get; set; }

        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI nicknameText;
        [SerializeField] protected Group group;

        public Group Group => group;
        public int Level => group.Level;

        public void SetLevelText()
        {
            levelText.text = $"Lv{group.Level}";
        }

        public void SetNicknameText()
        {
            nicknameText.text = group.Nickname;
        }

        public void SetUnit()
        {
            GrowStack = 1;

            SetLevelText();
            SetNicknameText();

            if (group.Level <= 200)
            {
                transform.localScale += new Vector3(0.01f, 0.01f, 0.01f) * Level;
            }
            else
            {
                transform.localScale = new Vector3(3f, 3f, 3f);
            }

            InitAddChick();
        }

        private void InitAddChick()
        {
            int chickCount = Level / 5;

            for (int i = 1; i < chickCount; i++)
            {
                group.AddChick();
            }
        }

        public virtual void Grow(int addValue)
        {
            group.GrowVFX();

            group.Level += addValue;
            GrowStack += addValue;

            if (group.Level < 200)
            {
                transform.localScale += new Vector3(0.01f, 0.01f, 0.01f) * addValue;
            }

            SetLevelText();

            if (GrowStack >= MAX_GROW)
            {
                GrowStack -= MAX_GROW;
                group.AddChick();
            }
        }
    }
}
