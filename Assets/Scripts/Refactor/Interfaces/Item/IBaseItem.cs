using UnityEngine;

namespace RC
{
    public interface IBaseItem : IBaseItemSave
    {
        public void SetUIItemSprite(GameObject itemUI);

        public void SetUIItemNameAndPrice(GameObject itemUI);

        public void SetUIItemNameAndOwned(GameObject itemUI);

        public void SetUIItemNameAndEquipped(GameObject itemUI);

        public void SetUIItemName(GameObject itemUI);

        public void DisableButton(GameObject itemUI);

        public void EnableButton(GameObject itemUI);

        public void SetUIItemAsOwned(GameObject itemUI);

        public void SetUIItemAsEquipped(GameObject itemUI);

        public abstract void SetUIForShop(GameObject itemUI);

        public abstract void SetUIForInventory(GameObject itemUI, bool equipped);

        public abstract void SetUIIItemStats(GameObject itemUI);
    }
}

