using UnityEngine;

public class Gate : MonoBehaviour
{
    public GateUnit GateUnits { get; private set; }
    public int Health { get; private set; }
    public UnitHandler Handler { get; private set; }
    public HealthSystem HealthSystem {  get; private set; }
    private bool _isSide;
    private HealthBar _healthBar;

    private Transform _currentTargetTransform;

    private SpriteRenderer _spriteRenderer;
    private string _healthBarPrefabPath = "Prefabs/HealthBar";
    private string _textureFolderPath = "Textures/Gate/";
    private string _gateFixSpritePath = "GateFix";
    private string _gateBrokenSpritePath = "GateBroken";
    public string ArrowProjectilePath = "Prefabs/Arrow";
    public string CanonballProjectilePath = "Prefabs/CanonBall";

    public void SpawnHealthBar()
    {
        HealthSystem healthSystem = new HealthSystem(100);
        HealthSystem = healthSystem;
        Health = healthSystem.GetHealth();
        _healthBar = Instantiate(Resources.Load<GameObject>(_healthBarPrefabPath).transform,
            transform.position + new Vector3(0, 1f, 0), Quaternion.identity, transform).GetComponent<HealthBar>();
        _healthBar.Setup(healthSystem);
    }

    public void GateSetUp(bool isSideGate)
    {
        SpawnHealthBar();
        GateUnits = new GateUnit();
        Handler = FindObjectOfType<GameCanvas>().UnitHandler;
        _isSide = isSideGate;
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public void TakeDamage(int damage)
    {
        HealthSystem.Damage(damage);
        Health = HealthSystem.GetHealth();
        if (Health < 1) Die();
    }

    public void SetUnit(int[] type, Hero.GateHero hero)
    {
        GateUnits.Archers = type[0];
        GateUnits.Canons = type[1];
        GateUnits.Hero = hero;
    }
    public int GetAvailableUnit(int type)
    {
        return type == 0 ? GateUnits.Archers : GateUnits.Canons;
    }
    public Hero.GateHero GetAvailableHero() => GateUnits.Hero;

    public void SetTarget(Transform targetTransform) => _currentTargetTransform = targetTransform;

    public void Die()
    {
        SwapSprite(HealthSystem.GetHealth());
        _healthBar.gameObject.SetActive(false);
        transform.GetComponent<Collider2D>().enabled = false;
    }

    private void SwapSprite(float health)
    {
        string includeSideString = _isSide ? "Side" : string.Empty;
        string gateString = (health > 0) ? _gateFixSpritePath : _gateBrokenSpritePath;
        _spriteRenderer.sprite = Resources.Load<Sprite>(_textureFolderPath + includeSideString + gateString);
    }


}
[System.Serializable]
public class GateUnit
{
    public int Archers = 0;
    public int Canons = 0;
    public int AllowedArchers = 0;
    public int AllowedCanons = 0;
    public float GateReloadSpeed = .3f;
    public float ArcherAttackSpeed = 1f;
    public float CanonAttackSpeed = 1f;
    public Hero.GateHero Hero;
    public GateType Type;
    public enum GateType
    {
        SMALLGATE,
        BIGGATE
    }
}

