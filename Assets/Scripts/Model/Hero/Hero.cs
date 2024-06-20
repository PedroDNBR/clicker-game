using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    HeroItem hero;

    public int health = 0;

    public event Action<Hero> onDestroyedAction;

    public event Action onTookDamageAction;

    public void TakeDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, hero.maxBaseHealth);
        if (onTookDamageAction != null) onTookDamageAction();
        if (health <= 0)
        {
            if (onDestroyedAction != null) onDestroyedAction(this);
        }
    }

    void SetHealth(int health)
    { 
        this.health = health; 
    }

    void SetSprite(Sprite sprite)
    {
        Image image = gameObject.GetComponent<Image>();
        image.sprite = sprite;
    }

    public void SetHero(HeroItem hero)
    {
        this.hero = hero;
        SetHealth(hero.maxBaseHealth);
        SetSprite(hero.sprite);
    }

    public HeroItem GetHero()
    {
        return hero;
    }

    public float GetHealthPercentage01()
    {
        return Mathf.InverseLerp(0, hero.maxBaseHealth, health);
    }
}
