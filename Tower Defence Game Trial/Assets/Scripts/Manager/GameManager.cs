using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool PlayerReady;
    private int _currentLevel = 0;
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
