using TMPro;
using UnityEngine;

namespace RC
{
    public class ViewLevel : MonoBehaviour
    {
        public TMP_Text levelText;
        public TMP_Text xpText;

        Level level;

        public void Init()
        {
            level = GetComponent<Level>();
            level.onXpChange += UpdateUI;
            UpdateUI();
        }

        void OnDisable()
        {
            level.onXpChange -= UpdateUI;
            UpdateUI();
        }

        void UpdateUI()
        {
            levelText.text = $"Level: {level.GetLevel()}";
            xpText.text = $"XP: {level.CurrentXp}";
        }
    }
}
