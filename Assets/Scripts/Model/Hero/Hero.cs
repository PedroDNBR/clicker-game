using System;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    HeroItem hero;

    int health = 0;

    public event Action<Hero> onDestroyedAction;

    public event Action onTookDamageAction;

    public void TakeDamage(int damage)
    {
        health = Mathf.Clamp(health - hero.GetTotalDamageToTake(damage), 0, hero.maxBaseHealth);
        if (onTookDamageAction != null) onTookDamageAction();
        hero.SetHealthWhenInField(health);
        if (health <= 0)
        {
            if (onDestroyedAction != null) onDestroyedAction(this);
        }
    }

    public void SetHealth(int health)
    { 
        this.health = health; 
    }

    public void SetHealthToMax()
    {
        if (hero == null) return;
        this.health = hero.maxBaseHealth;
        if (onTookDamageAction != null) onTookDamageAction();
    }

    void SetSprite(Sprite sprite)
    {
        Image image = gameObject.GetComponent<Image>();
        image.sprite = sprite;
    }

    public void SetHeroItem(HeroItem hero)
    {
        this.hero = hero;
        if(hero.GetHealthWhenInField() > 0)
            SetHealth(hero.GetHealthWhenInField());
        else
            SetHealth(hero.maxBaseHealth);

        SetSprite(hero.largeSprite);
    }

    public HeroItem GetHeroItem()
    {
        return hero;
    }

    public float GetHealthPercentage01()
    {
        return Mathf.InverseLerp(0, hero.maxBaseHealth, health);
    }

    public int GetHealth()
    {
        return health;
    }
}
