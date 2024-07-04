using System;
using UnityEngine;

namespace RC
{
    public class HeroController : MonoBehaviour, IHeroController
    {
        public static HeroController instance;

        public Hero Hero { get; set; }

        public Hero heroTemplate;

        public Transform spawnerPivot;

        public Action OnHeroSpawned { get; set; }

        public HeroController()
        {
            instance = this;
        }

        public void HeroWasDestroyed(IHero hero, string localId)
        {
            hero.onHeroDestroyedAction -= HeroWasDestroyed;
            Inventory.instance.GetHeroItemByLocalId(localId).Owned = false;
            Inventory.instance.SetHeroInField(null);
            Inventory.instance.HeroesId.Remove(localId);

            if(!string.IsNullOrEmpty((hero.CharacterItem as HeroItem).WeaponId))
                Inventory.instance.WeaponsId.Remove((hero.CharacterItem as HeroItem).WeaponId);

            if (!string.IsNullOrEmpty((hero.CharacterItem as HeroItem).HelmetId))
                Inventory.instance.HelmetsId.Remove((hero.CharacterItem as HeroItem).HelmetId);

            if (!string.IsNullOrEmpty((hero.CharacterItem as HeroItem).ChestplateId))
                Inventory.instance.ChestplatesId.Remove((hero.CharacterItem as HeroItem).ChestplateId);

            if (!string.IsNullOrEmpty((hero.CharacterItem as HeroItem).LeggingsId))
                Inventory.instance.LeggingsId.Remove((hero.CharacterItem as HeroItem).LeggingsId);

            if (!string.IsNullOrEmpty((hero.CharacterItem as HeroItem).BootsId))
                Inventory.instance.BootsId.Remove((hero.CharacterItem as HeroItem).BootsId);

            Destroy((hero as Hero).gameObject);
        }

        public void HitHeroInField(int damage)
        {
            Hero.TakeDamage(damage);
        }

        public void SpawnHero(IHeroItem heroItem, string localId)
        {
            if (Hero != null)
            {
                (Hero.CharacterItem as HeroItem).HealthWhenInField = Hero.Health;
                Destroy(Hero.gameObject);
            }
            Hero = Instantiate(heroTemplate, spawnerPivot);
            Hero.CharacterItem = heroItem;
            Hero.localHeroId = localId;
            if (OnHeroSpawned != null) OnHeroSpawned();
            Hero.onHeroDestroyedAction += HeroWasDestroyed;
        }
    }
}
