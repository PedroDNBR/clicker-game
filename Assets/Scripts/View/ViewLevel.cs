using TMPro;
using UnityEngine;

public class ViewLevel : MonoBehaviour
{
    public TMP_Text levelText;
    public TMP_Text xpText;

    public Level level;

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
        xpText.text = $"XP: {level.GetXp()}";
    }
}
