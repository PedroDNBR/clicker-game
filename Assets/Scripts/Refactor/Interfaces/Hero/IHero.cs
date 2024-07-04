using System;

namespace RC
{
    public interface IHero : ICharacter
    {
        public void SetHealthToMax();

        public event Action<IHero, string> onHeroDestroyedAction;

    }
}

