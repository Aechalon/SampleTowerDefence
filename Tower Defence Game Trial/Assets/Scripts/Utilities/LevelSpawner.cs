using UnityEngine;
using System.Collections;
public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private int _currentLevel;
    [SerializeField] private float _countdown;
    [SerializeField] private GameObject _spawnPoint;

    public Wave[] Waves;
    public Level CurrentLevel {  get; private set; }
    public int CurrentWaveIndex { get; private set; }
    public int TotalMobs { get; private set; }
    public int TotalWaves { get; private set; }

    private bool _readyToCountDown;
    private void Start()
    {     
        CurrentLevel = LevelManager.Instance.GetLevel(_currentLevel);
    }
    public void PlayerReady()
    {
        _readyToCountDown = true;
        GameManager.Instance.PlayerReady = true;
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        foreach (Wave wave in Waves)
            foreach (Mob mob in wave.Mobs)
                TotalMobs++;

        yield return new WaitForSeconds(_countdown);

        while(TotalWaves < Waves.Length)
        {
            StartCoroutine(SpawnWave());
            TotalWaves += 1;
            yield return new WaitForSeconds(CurrentLevel.TimeToNextWave);
        }
        if(LevelManager.Instance.MobsKilled >= TotalMobs)
        {
            LevelManager.Instance.LevelComplete();
            GameManager.Instance.PlayerReady = false;
            yield return null;
        }
    }

    private IEnumerator SpawnWave()
    {
        if (CurrentWaveIndex < Waves.Length)
        {
            for (int i = 0; i < Waves[CurrentWaveIndex].Mobs.Length; i++)
            {
                Instantiate(Waves[CurrentWaveIndex].Mobs[i],
                    _spawnPoint.transform.position, Quaternion.identity);

                yield return new WaitForSeconds(CurrentLevel.TimeToNextEnemy);
            }
        }
    }
}

[System.Serializable]
public class Wave
{
    public Mob[] Mobs;

}