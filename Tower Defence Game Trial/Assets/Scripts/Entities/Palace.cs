using UnityEngine;

public class Palace : MonoBehaviour
{
    public int AmountOfMobsAllowed = 5;
    private int _totalMobs = 0;
    [SerializeField]
    public GameObject GameOverPanel;
    MobInsideDelegate MobInside;
    private delegate void MobInsideDelegate();

    private void Awake()
    {
        MobInside += CheckMobCount;
        GameOverPanel.SetActive(false);
    }
    private void CheckMobCount()
    {
        if (_totalMobs < AmountOfMobsAllowed) return;

        Application.ExternalCall("CallPalace", "Mob");
        Time.timeScale = 0;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Mob mob))
        {
            _totalMobs += 1;
            mob.TakeDamage(999);
            MobInside();
   
        }
    }
}
