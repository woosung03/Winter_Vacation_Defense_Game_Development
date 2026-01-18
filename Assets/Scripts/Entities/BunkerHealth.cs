using System;
using UnityEngine;

namespace Entities
{
    public class BunkerHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private bool destroyOnDeath = false;

        public int CurrentHealth { get; private set; }
        public int MaxHealth => maxHealth;

        // UI 등이 구독할 이벤트
        public event Action<int, int> OnHealthChanged; // (current, max)

        private void Awake()
        {
            CurrentHealth = maxHealth;
            OnHealthChanged?.Invoke(CurrentHealth, maxHealth); // 초기값 통지
        }

        public void TakeDamage(int amount)
        {
            if (amount <= 0) return;

            CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
            Debug.Log($"[Bunker] HP: {CurrentHealth}/{maxHealth}");

            OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

            if (CurrentHealth <= 0)
                Die();
        }

        private void Die()
        {
            Debug.Log("[Bunker] Destroyed (Game Over placeholder).");

            if (destroyOnDeath)
                Destroy(gameObject);
        }
    }
}
