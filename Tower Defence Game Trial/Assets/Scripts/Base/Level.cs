using UnityEngine;
[CreateAssetMenu(fileName ="Level", menuName ="Level/CreateLevel", order = 1)]
public class Level : ScriptableObject
{
    public int LevelIndex = 1;
    public int NumberOfWaves = 60;
    public float TimeToNextEnemy;
    public float TimeToNextWave;
}
