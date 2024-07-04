namespace RC
{
    public interface IWeaponItem : IBaseItem
    {
        public int AttackPowerBoost { get; set; }
        public float AttackSpeedBoost { get; set; }
    }
}