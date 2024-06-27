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
        int indexToRemove = Inventory.instance.heroes.FindIndex(heroInList => heroInList.shoppedItemCount == hero.GetHeroItem().shoppedItemCount);
        Inventory.instance.heroes.RemoveAt(indexToRemove);
        if (hero.GetHeroItem().weapon != null) Inventory.instance.weapons.RemoveAt(Inventory.instance.weapons.FindIndex(weaponInList => weaponInList.shoppedItemCount == hero.GetHeroItem().weapon.shoppedItemCount));
        if (hero.GetHeroItem().helmet != null) Inventory.instance.helmets.RemoveAt(Inventory.instance.helmets.FindIndex(helmetsInList => helmetsInList.shoppedItemCount == hero.GetHeroItem().helmet.shoppedItemCount));
        if (hero.GetHeroItem().chestplate != null) Inventory.instance.chestplates.RemoveAt(Inventory.instance.chestplates.FindIndex(chestplatesInList => chestplatesInList.shoppedItemCount == hero.GetHeroItem().chestplate.shoppedItemCount));
        if (hero.GetHeroItem().leggings != null) Inventory.instance.leggings.RemoveAt(Inventory.instance.leggings.FindIndex(leggingsInList => leggingsInList.shoppedItemCount == hero.GetHeroItem().leggings.shoppedItemCount));
        if (hero.GetHeroItem().boots != null) Inventory.instance.boots.RemoveAt(Inventory.instance.boots.FindIndex(bootsInList => bootsInList.shoppedItemCount == hero.GetHeroItem().boots.shoppedItemCount));
        Destroy(hero.gameObject);
    }

    public void HitHeroInField(int damage)
    {
        hero.TakeDamage(damage);
    }

    public Hero GetHero()
    {
        return hero;
    }
}
