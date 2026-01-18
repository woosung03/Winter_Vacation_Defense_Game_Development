using UnityEngine;

namespace Systems
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private GameObject enemyPrefab;

        [Header("Spawn")]
        [SerializeField] private float spawnInterval = 1.5f;
        [SerializeField] private float extraRadius = 2.0f; // 화면 밖으로 얼마나 더 바깥에서 스폰할지

        private float timer;
        private Camera cam;

        private void Awake()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            if (enemyPrefab == null || cam == null) return;

            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                timer = 0f;
                Spawn();
            }
        }

        private void Spawn()
        {
            // 카메라 기준 화면 반지름 계산 (Orthographic 카메라 기준)
            float height = cam.orthographicSize;
            float width = height * cam.aspect;
            float radius = Mathf.Sqrt(width * width + height * height) + extraRadius;

            float angle = Random.Range(0f, Mathf.PI * 2f);
            Vector2 center = cam.transform.position;
            Vector2 pos = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

            Instantiate(enemyPrefab, pos, Quaternion.identity);
        }
    }
}
