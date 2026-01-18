using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed = 12f;
        [SerializeField] private float lifeTime = 3f;

        private int damage;
        private Vector2 direction;

        private float timer;

        public void Init(Vector2 dir, int dmg)
        {
            direction = dir.normalized;
            damage = dmg;
            timer = 0f;
        }

        private void Awake()
        {
            var rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.gravityScale = 0f;

            var col = GetComponent<Collider2D>();
            col.isTrigger = true;
        }

        private void Update()
        {
            transform.position += (Vector3)(direction * speed * Time.deltaTime);

            timer += Time.deltaTime;
            if (timer >= lifeTime)
                Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Enemy")) return;

            var hp = other.GetComponent<EnemyHealth>();
            if (hp != null)
                hp.TakeDamage(damage);

            // 데미지가 적 체력보다 크든 작든, 충돌하면 총알은 사라짐
            Destroy(gameObject);
        }
    }
}
