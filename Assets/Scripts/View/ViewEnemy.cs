using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewEnemy : MonoBehaviour
{
    public Enemy enemy;

    public Image healthBarFill;

    private void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        enemy.onTookDamageAction += UpdateUI;
    }

    void UpdateUI()
    {
        healthBarFill.fillAmount = enemy.GetHealthPercentage01();
    }
}
