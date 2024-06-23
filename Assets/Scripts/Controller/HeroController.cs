using System;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public static HeroController instance;

    Hero hero;

    public Hero heroTemplate;

    public Transform spawnerPivot;

    public Action OnHeroSpawned;

    public HeroController()
    {
        instance = this;
    }

    public void SpawnHero(HeroItem heroItem)
    {
        if (hero != null) 
        {
            hero.GetHeroItem().SetHealthWhenInField(hero.GetHealth());
            Destroy(hero.gameObject);
        }
        hero = Instantiate(heroTemplate, spawnerPivot);
        hero.SetHeroItem(heroItem);
        if (OnHeroSpawned != null) OnHeroSpawned();
        hero.onDestroyedAction += HeroWasDestroyed;
    }

    public void HeroWasDestroyed(Hero hero)
    {
        hero.onDestroyedAction -= HeroWasDestroyed;
        Shop.instance.heroes[hero.GetHeroItem().GetShopIndex()].SetOwned(false);
        Inventory.instance.SetHeroInField(null);
        Inventory.instance.heroes.Remove(hero.GetHeroItem());
        if (hero.GetHeroItem().weapon != null) Inventory.instance.weapons.Remove(hero.GetHeroItem().weapon);
        if (hero.GetHeroItem().helmet != null) Inventory.instance.helmets.Remove(hero.GetHeroItem().helmet);
        if (hero.GetHeroItem().chestplate != null) Inventory.instance.chestplates.Remove(hero.GetHeroItem().chestplate);
        if (hero.GetHeroItem().leggings != null) Inventory.instance.leggings.Remove(hero.GetHeroItem().leggings);
        if (hero.GetHeroItem().boots != null) Inventory.instance.boots.Remove(hero.GetHeroItem().boots);
        Destroy(hero.gameObject);
    }

    public void HitHeroInField(int damage)
    {
        hero.TakeDamage(damage);
    }
}
