using UnityEngine;
using TMPro;
using Entities;

namespace Systems
{
    public class WaveManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private EnemySpawner spawner;
        [SerializeField] private TMP_Text waveText;

        [Header("Wave Settings")]
        [SerializeField] private int baseEnemyCount = 5;
        [SerializeField] private float baseSpawnInterval = 1.2f;

        public int CurrentWave { get; private set; } = 0;
        private bool isSpawning;

        private void Start()
        {
            StartNextWave();
        }

        public void StartNextWave()
        {
            if (isSpawning) return;

            CurrentWave++;
            UpdateWaveUI();

            int enemyCount = baseEnemyCount + CurrentWave * 2;
            float spawnInterval = Mathf.Max(0.3f, baseSpawnInterval - CurrentWave * 0.03f);

            StartCoroutine(SpawnWave(enemyCount, spawnInterval));
        }

        private System.Collections.IEnumerator SpawnWave(int count, float interval)
        {
            isSpawning = true;

            for (int i = 0; i < count; i++)
            {
                spawner.SpawnScaledEnemy(CurrentWave);
                yield return new WaitForSeconds(interval);
            }

            isSpawning = false;
        }

        private void UpdateWaveUI()
        {
            if (waveText != null)
                waveText.text = $"Wave {CurrentWave}";
        }
    }
}
