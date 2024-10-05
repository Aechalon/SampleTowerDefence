using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public void Setup(HealthSystem healthSystem)
    {
        _healthSystem = healthSystem;
        _healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }
    private HealthSystem _healthSystem;
    private Transform _healthBarTransform;

    private void Awake()
    {
        _healthBarTransform = transform.GetChild(1).transform;
    }
    private void Start()
    {
        SetHealtBarColor();
    }
    private void SetHealtBarColor()
    {
                _healthBarTransform.GetChild(0)
        .GetComponent<SpriteRenderer>().color =
        transform.parent.GetComponent<Gate>() ?
        LevelColorManager.Instance.GateHBColor :
        LevelColorManager.Instance.EnemyHBColor;

    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e) => 
        _healthBarTransform.localScale = new Vector3(_healthSystem.GetHealthPercent(),1,1);
}
