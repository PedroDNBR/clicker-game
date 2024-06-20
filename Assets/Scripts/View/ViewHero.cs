using System.Collections;
using System.Collections.Generic;
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
    }

    void UpdateUI()
    {
        healthBarFill.fillAmount = hero.GetHealthPercentage01();
    }
}
