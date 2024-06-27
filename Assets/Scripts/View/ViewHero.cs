using UnityEngine;
using UnityEngine.UI;

public class ViewHero : MonoBehaviour
{
    public Hero hero;

    public Image healthBarFill;

    private void Start()
    {
        hero = gameObject.GetComponent<Hero>();
        hero.onTookDamageAction += UpdateUI;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (hero == null) return;
        healthBarFill.fillAmount = hero.GetHealthPercentage01();
    }
}
