using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2.5f;
        [SerializeField] private int contactDamage = 10;

        private Transform bunker;

        private void Awake()
        {
            // Rigidbody2D 안전 세팅
            var rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.gravityScale = 0f;
        }

        private void Start()
        {
            var bunkerObj = GameObject.FindGameObjectWithTag("Bunker");
            if (bunkerObj != null) bunker = bunkerObj.transform;
        }

        private void Update()
        {
            if (bunker == null) return;

            Vector2 dir = ((Vector2)bunker.position - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Bunker")) return;

            var health = other.GetComponent<BunkerHealth>();
            if (health != null)
                health.TakeDamage(contactDamage);

            Destroy(gameObject);
        }
    }
}
