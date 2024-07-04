namespace RC
{
    public interface IBaseCharacter : IBaseItem
    {
        public int MaxBaseHealth { get; set; }
        public int BaseAttackPower { get; set; }
        public float BaseAttackSpeed { get; set; }
    }
}
