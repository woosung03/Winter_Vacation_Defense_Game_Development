using UnityEngine;

namespace Entities
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1.5f;
        private Transform target;

        private void Start()
        {
            // 벙커(또는 타겟) 태그를 "Bunker"로 사용한다고 가정
            GameObject bunker = GameObject.FindGameObjectWithTag("Bunker");
            if (bunker != null) target = bunker.transform;
        }

        private void Update()
        {
            if (target == null) return;

            Vector3 dir = (target.position - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;
        }

        public void SetSpeed(float value)
        {
            moveSpeed = Mathf.Max(0.1f, value);
        }
    }
}

