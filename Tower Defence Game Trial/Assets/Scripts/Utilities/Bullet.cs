using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 10f;
    public int Damage {  get; private set; }    
    public Transform Target {  get; private set; }
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();  
    }

    private void Update()
    {
        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }
        transform.right = Target.position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, Target.position, _bulletSpeed * Time.deltaTime);
    }
    
    public void Setup(Transform target, int damage)
    {
        Target = target;
        Damage = damage;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Mob mob))
        {
            mob.TakeDamage(Damage);
            Destroy(gameObject);    
        }
    }

}
