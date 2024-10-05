public class Unit
{
    UnitType BasicUnit;
    enum UnitType
    {
        Archer,
        Canon,
    }

    public int Damage;
    public int Range;
    public int AttackSpeed;
}

public class Archer 
{
    public static int Damage = 20;
    public static float AttackSpeed = .5f;

}
public class Canon
{
    public static int Damage = 40;
    public static float AttackSpeed = 1f;

}
