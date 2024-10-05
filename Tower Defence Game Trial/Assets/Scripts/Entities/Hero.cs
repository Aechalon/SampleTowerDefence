public class Hero
{
    public enum GateHero
    {
        NoHero,
        ArcherHero,
        BomberHero,
        HealerHero,
    }

}
public class ArcherHero  : Hero
{
    public int Damage = 30;
    public float AttackSpeed = .3f;
    public float AttackSpeedBuff = .20f;
    public float DefenceBuff = 0f;
    public float DamageBuff = 0f;

}
public class HealerHero : Hero
{
    public int Damage = 20;
    public float AttackSpeed = 0f;
    public float AttackSpeedBuff = 0f;
    public float DefenceBuff = .10f;
    public float DamageBuff = 0;
}
public class BomberHero : Hero
{
    public int Damage = 80;
    public float AttackSpeed = 0f;
    public float AttackSpeedBuff = 0f;
    public float DefenceBuff = 0f;
    public float DamageBuff = .10f;

}
