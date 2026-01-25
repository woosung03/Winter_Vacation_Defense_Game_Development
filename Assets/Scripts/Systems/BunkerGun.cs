using UnityEngine;
using Entities;

namespace Systems
{
    public class BunkerGun : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform firePoint;
        [SerializeField] private Bullet bulletPrefab;

        [Header("Fire")]
        [SerializeField] private float fireInterval = 0.25f;
        [SerializeField] private int bulletDamage = 10;
        [SerializeField] private float range = 10f;

        public float Range => range;

        private float timer;

        public void ApplyUpgrade(float fireIntervalMul, int damageAdd, float rangeAdd)
        {
            fireInterval = Mathf.Max(0.05f, fireInterval * fireIntervalMul);
            bulletDamage = Mathf.Max(1, bulletDamage + damageAdd);
            range = Mathf.Max(0.5f, range + rangeAdd);
        }

        private void Update()
        {
            if (bulletPrefab == null) return;

            timer += Time.deltaTime;
            if (timer < fireInterval) return;

            Transform target = FindClosestEnemy();
            if (target == null) return;

            Fire(target);
            timer = 0f;
        }

        /// <summary>
        /// 사거리 안에서 가장 가까운 적 찾기
        /// </summary>
        private Transform FindClosestEnemy()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0) return null;

            Vector2 origin = transform.position;
            float bestDistSqr = range * range;

            Transform bestTarget = null;

            foreach (var enemy in enemies)
            {
                Vector2 diff = (Vector2)enemy.transform.position - origin;
                float distSqr = diff.sqrMagnitude;

                if (distSqr <= bestDistSqr)
                {
                    bestDistSqr = distSqr;
                    bestTarget = enemy.transform;
                }
            }

            return bestTarget;
        }

        private void Fire(Transform target)
        {
            Vector2 origin = firePoint != null
                ? (Vector2)firePoint.position
                : (Vector2)transform.position;

            Vector2 dir = ((Vector2)target.position - origin).normalized;

            Bullet bullet = Instantiate(bulletPrefab, origin, Quaternion.identity);
            bullet.Init(dir, bulletDamage);
        }
    }
}
