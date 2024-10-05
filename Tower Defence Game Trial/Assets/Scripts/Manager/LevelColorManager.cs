using UnityEngine;

public class LevelColorManager : MonoBehaviour
{
    public Color GateHBColor, EnemyHBColor;
    public static LevelColorManager Instance;
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

    }
}
