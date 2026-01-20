using UnityEngine;
using Systems;

namespace Entities
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int maxHP = 10;
        [SerializeField] private int goldReward = 10;

        private int currentHP;
        private UpgradeManager upgradeManager;

        private void Awake()
        {
            currentHP = maxHP;
            upgradeManager = FindObjectOfType<UpgradeManager>();
        }

        public void TakeDamage(int damage)
        {
            currentHP -= damage;

            if (currentHP <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (upgradeManager != null)
            {
                upgradeManager.AddGold(goldReward);
            }

            Destroy(gameObject);
        }
    }
}
