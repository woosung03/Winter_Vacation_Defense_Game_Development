using UnityEngine;
using Entities;

namespace Systems
{
    [CreateAssetMenu(fileName = "WaveConfig", menuName = "Game/Wave Config")]
    public class WaveConfig : ScriptableObject
    {
        [Header("Wave Scaling")]
        [SerializeField] private int baseEnemyCount = 5;
        [SerializeField] private float baseSpawnInterval = 1.2f;
        [SerializeField] private float countGrowthPerWave = 2f;
        [SerializeField] private float intervalReductionPerWave = 0.03f;
        [SerializeField] private float minInterval = 0.3f;

        [Header("Enemy Growth")]
        public float hpGrowthPerWave = 0.25f;
        public float speedGrowthPerWave = 0.05f;

        [Header("Enemy Type Chances")]
        [Range(0f, 1f)]
        public float tankEnemyChance = 0.2f;
        [Range(0f, 1f)]
        public float fastEnemyChance = 0.3f;

        public int GetCount(int wave)
        {
            return baseEnemyCount + Mathf.RoundToInt(wave * countGrowthPerWave);
        }

        public float GetInterval(int wave)
        {
            return Mathf.Max(minInterval, baseSpawnInterval - wave * intervalReductionPerWave);
        }
    }
}
