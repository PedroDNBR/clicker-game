namespace RC
{
    public interface IBaseItemSave
    {
        string ItemName { get; set; }

        string SpritePath { get; set; }

        string SmallSpritePath { get; set; }

        int Price { get; set; }

        int ShoppedItemCount { get; set; }

        int MinLevelToUnlock { get; set; }

        ItemTypes ItemType { get; set;}

    }
}
