using UnityEngine;
using Entities;

namespace Systems
{
    [System.Serializable]
    public class EnemyEntry
    {
        public EnemyType type;
        public GameObject prefab;
    }

    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyEntry[] enemyEntries;
        [SerializeField] private GameObject defaultEnemyPrefab;
        [SerializeField] private float extraRadius = 2.0f; // 화면 밖으로 얼마나 더 바깥에서 스폰할지

        private Camera cam;

        private void Awake()
        {
            cam = Camera.main;
        }

        public void SpawnEnemy(EnemyType type)
        {
            if (enemyEntries == null || enemyEntries.Length == 0) return;
            if (cam == null) cam = Camera.main;

            foreach (var entry in enemyEntries)
            {
                if (entry != null && entry.type == type && entry.prefab != null)
                {
                    Instantiate(entry.prefab, GetSpawnPosition(), Quaternion.identity);
                    return;
                }
            }
        }

        // 웨이브 번호에 따라 적 타입을 순차/스케일링 스폰
        public void SpawnScaledEnemy(int wave)
        {
            if (cam == null) cam = Camera.main;

            if (defaultEnemyPrefab == null) return;

            GameObject enemy = Instantiate(defaultEnemyPrefab, GetSpawnPosition(), Quaternion.identity);

            EnemyStats stats = enemy.GetComponent<EnemyStats>();
            if (stats != null)
            {
                stats.ApplyWaveScaling(wave);
            }
        }

        private Vector3 GetSpawnPosition()
        {
            if (cam == null) return transform.position;

            // 기존 원형 스폰 로직 그대로 (Orthographic 카메라 기준)
            float height = cam.orthographicSize;
            float width = height * cam.aspect;
            float radius = Mathf.Sqrt(width * width + height * height) + extraRadius;

            float angle = Random.Range(0f, Mathf.PI * 2f);
            Vector2 center = cam.transform.position;
            Vector2 pos = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

            return pos;
        }

    }
}
