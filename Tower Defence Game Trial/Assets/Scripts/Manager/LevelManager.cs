using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level[] Level;

    private string _levelPath = "Data/Levels/";
    private string _ordinatePath = "Data/Coordinates/Ordinate";

    public static LevelManager Instance;
    public int MobsKilled {  get; private set; }
    public ObjectOrdinate ObjectOrdinate { get; private set; }


    private void Awake()
    {
        #region Singleton
        if (Instance != null && Instance == this)
        {
            Destroy(this.gameObject);
            return;
        }
        else { Instance = this; }
        #endregion
        Level = Resources.LoadAll<Level>(_levelPath);
        ObjectOrdinate = Resources.Load<ObjectOrdinate>(_ordinatePath);
    }
    public Level GetLevel(int level) => Level[level];

    public void MobKilled() => MobsKilled += 1;

    public void LevelComplete()
    {
        ResetMobCount();
    }

    public void ResetMobCount() => MobsKilled = 0;

}
