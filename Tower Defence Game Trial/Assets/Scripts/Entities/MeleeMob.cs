using System.Collections;
using UnityEngine;

public class MeleeMob : Mob
{
    public Gate TargetGate { get; private set; }
    public Mob ThisMob { get; private set; }

    private void Awake()
    {
        ThisMob = GetComponent<Mob>();
    }

    private IEnumerator AttackTarget()
    {
        while (TargetGate.Health > 0)
        {
            ThisMob.DisableMovement(true);
            TargetGate.TakeDamage(Damage);
            yield return new WaitForSeconds(AttackSpeed);

        }
        if(TargetGate.Health <= 0)
        {
            ThisMob.DisableMovement(false);
            yield return null;
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Gate gate))
        {
            TargetGate = gate;
            StartCoroutine(AttackTarget());

        }
    }
}
