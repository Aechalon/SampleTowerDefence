using System;

public class HealthSystem 
{
    public event EventHandler OnHealthChanged;
    private int _health;
    private int _healthMax;

    public HealthSystem(int healthMax) {  _healthMax = healthMax;
        _health = healthMax;
    }

    public int GetHealth() => _health;

    public float GetHealthPercent() => (float)_health / _healthMax;

    public void Damage(int damage)
    {
        _health = (_health < 0) ? 0 : _health - damage;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);     
    }
    public void Heal(int heal)
    {
        _health = (_health > _healthMax) ? _healthMax : _health + heal;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }
}
