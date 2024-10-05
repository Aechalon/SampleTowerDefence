using UnityEngine;
using System.Collections;
public class BigGate : Gate
{
    [Header("Variables")]
    [SerializeField] private int _gateRadius = 3;
    [SerializeField] private int _allowedArchers = 100;
    [SerializeField] private int _allowedCanons = 2;
    [SerializeField] private bool _isSideGate;
    public bool HasHero { get; private set; }
    public Gate Gate { get; private set; }
    private Mob _targetMob;

    private void Awake()
    {
        Gate = GetComponent<Gate>();
        Gate.GateSetUp(_isSideGate);
        GetComponent<CircleCollider2D>().radius = _gateRadius;
        Gate.GateUnits.AllowedCanons = _allowedCanons;
        Gate.GateUnits.AllowedArchers = _allowedArchers;
        Gate.GateUnits.Type = GateUnit.GateType.BIGGATE;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Mob mob))
        {
            _targetMob = mob;
            if (Gate.HealthSystem.GetHealth() < 1) return;
            StartCoroutine(AttackTarget());
        }
    }
    public void OnMouseDown()
    {
        OnMouseClick();
    }

    public void OnMouseClick()
    {
        if (GameManager.Instance.PlayerReady) return;
        Handler.gameObject.SetActive(true);
        Handler.SetSelectedGate(Gate, GateUnits.Type.ToString());
        Handler.SetRestriction(_allowedArchers, _allowedCanons);
    
    }
    private IEnumerator AttackTarget()
    {
        while (_targetMob.Health > 0)
        {
            StartCoroutine(ArcherShoot());
            StartCoroutine(CanonShoot());

            yield return new WaitForSeconds(Gate.GateUnits.GateReloadSpeed);
        }


    }
    private IEnumerator ArcherShoot()
    {
        for (int i = 0; i < Gate.GateUnits.Archers; i++)
            Instantiate(Resources.Load<GameObject>(Gate.ArrowProjectilePath), transform.position, Quaternion.identity)
                 .GetComponent<Bullet>().Setup(_targetMob.transform, Archer.Damage);


        yield return new WaitForSeconds(Gate.GateUnits.ArcherAttackSpeed);
    }
    private IEnumerator CanonShoot()
    {
        for (int i = 0; i < Gate.GateUnits.Canons; i++)
            Instantiate(Resources.Load<GameObject>(Gate.CanonballProjectilePath), transform.position, Quaternion.identity)
                .GetComponent<Bullet>().Setup(_targetMob.transform, Canon.Damage);
        yield return new WaitForSeconds(Gate.GateUnits.CanonAttackSpeed);
    }
}
