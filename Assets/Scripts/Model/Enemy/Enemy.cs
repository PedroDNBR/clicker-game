using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IEnemy
{
    EnemyItem enemyItem;

    int health;

    int index = -1;

    public event Action<Enemy> onDestroyedAction;

    public event Action onTookDamageAction;

    public void TakeDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, enemyItem.maxBaseHealth);
        if (onTookDamageAction != null) onTookDamageAction();
        if (health <= 0)
        {
            if (onDestroyedAction != null) onDestroyedAction(this);
        }
    }

    public int GetMaxHealth()
    {
        return enemyItem.maxBaseHealth;
    }

    public float GetHealthPercentage01()
    {
        return Mathf.InverseLerp(0, enemyItem.maxBaseHealth, health);
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public void Heal(int heal)
    {
        health = Mathf.Clamp(health + heal, 0, enemyItem.maxBaseHealth);
    }

    public void SetEnemyItem(EnemyItem enemy)
    {
        enemyItem = enemy;
        health = enemyItem.maxBaseHealth;
        GetComponent<Image>().sprite = EnemySpawner.instance.enemiesLargeSpriteList[enemyItem.largeSpriteIndex];
    }

    public int GetIndex()
    {
        return index;
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }

    public EnemyItem GetEnemyItem()
    {
        return enemyItem;
    }
}
