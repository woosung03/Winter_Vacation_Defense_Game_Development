using UnityEngine;
using Systems;

namespace Entities
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int maxHP = 10;
        [SerializeField] private int goldReward = 10;
        [SerializeField] private float metaDropChance = 0.05f; // 5% È®·ü
        [SerializeField] private int metaReward = 1;

        private int currentHP;
        private UpgradeManager upgradeManager;

        private void Awake()
        {
            currentHP = maxHP;
            upgradeManager = FindAnyObjectByType<UpgradeManager>();
        }

        public void TakeDamage(int damage)
        {
            currentHP -= damage;

            if (currentHP <= 0)
            {
                Die();
            }
        }

        public void SetMaxHP(int value)
        {
            maxHP = value;
            currentHP = maxHP;
        }

        private void Die()
        {
            upgradeManager?.AddGold(goldReward);

            if (Random.value < metaDropChance)
            {
                MetaCurrencyManager.Instance.Add(metaReward);
            }

            Destroy(gameObject);
        }
    }
}
