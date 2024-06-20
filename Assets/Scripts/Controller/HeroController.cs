using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    Hero hero;

    public Hero heroTemplate;

    public Transform spawnerPivot;

    public Action OnHeroSpawned;

    public void SpawnHero(HeroItem heroItem)
    {
        if (hero != null) 
        { 
            Destroy(hero.gameObject);
        }
        hero = Instantiate(heroTemplate, spawnerPivot);
        hero.SetHero(heroItem);
        if (OnHeroSpawned != null) OnHeroSpawned();
    }

    public void HitHeroInField(int damage)
    {
        hero.TakeDamage(damage);
    }
}
