namespace RC
{
    public interface IEnemyItem : IBaseCharacter
    {
        public int Xp { get; set; }
        public int Gold { get; set; }

        public int LargeSpriteIndex { get; set; }
    }
}
