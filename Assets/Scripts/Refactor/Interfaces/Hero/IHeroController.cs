using System;
using UnityEngine;

namespace RC
{
    public interface IHeroController
    {
        public Hero Hero { get; set; }

        public Action OnHeroSpawned { get; set; }

        public void SpawnHero(IHeroItem heroItem, string localId);

        public void HeroWasDestroyed(IHero hero, string localId);

        public void HitHeroInField(int damage);
    }
}
