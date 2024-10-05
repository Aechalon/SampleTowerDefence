using System.Collections;
using UnityEngine;

public class Mob : MonoBehaviour
{
    [Header("Status")]
    private Rigidbody2D _rigidbody;
    public int Health { get; private set; }
    public int Damage = 20;
    public float Speed = .2f;
    public float AttackSpeed = 3f;


    private int _pathIndex = 0;
    private int _pathLength = 0;
    private int _ordinateIndex = 0;

    private HealthBar _healthBar;
    private string _healthBarPrefabPath = "Prefabs/HealthBar";

    public HealthSystem HealthSystem { get; private set; }

    private bool _stopMoving = false;
    private Vector3 _destination;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _ordinateIndex = Random.Range(0, LevelManager.Instance.ObjectOrdinate.Ordinate.Length);
        _pathLength = LevelManager.Instance.ObjectOrdinate.Ordinate[_ordinateIndex].Checkpoint.Length;
        HealthSystem healthSystem = new HealthSystem(100);
        HealthSystem = healthSystem;
        Health = healthSystem.GetHealth();
        Transform healthBarTransform = Instantiate(Resources.Load<GameObject>(_healthBarPrefabPath).transform,
            transform.position + new Vector3(0,.5f,0) , Quaternion.identity, transform);
        _healthBar = healthBarTransform.GetComponent<HealthBar>();
        _healthBar.Setup(healthSystem);
        
        SetDestination();
    }

    private void FixedUpdate()
    {
        if(_stopMoving) return;
        Vector3 direction  = (_destination - transform.position).normalized;
        _rigidbody.velocity = Speed * direction;
 
        if (Vector3.Distance(_destination, transform.position) <= 0.1f)
        {
            _pathIndex += 1;
            SetDestination();
        }

    }
    
    public void DisableMovement(bool enable)
    {
        _stopMoving = enable;
        _rigidbody.velocity = Vector3.zero;
    }

    public void SetDestination()
    {
        if (_pathIndex == _pathLength)
            Die();
        else
            _destination = LevelManager.Instance.ObjectOrdinate.Ordinate[_ordinateIndex].Checkpoint[_pathIndex];
    }

    public void TakeDamage(int damage)
    {
        HealthSystem.Damage(damage);
        Health = HealthSystem.GetHealth();
        if (Health < 1) Die();
    }


    private void Die()
    {
       _healthBar.gameObject.SetActive(false);
       Destroy(gameObject);
    }

 


}
