using UnityEngine;

namespace Entities
{
    public class EnemyStats : MonoBehaviour
    {
        [Header("Base Stats")]
        [SerializeField] private int baseHP = 10;
        [SerializeField] private float baseSpeed = 1.5f;

        public void ApplyWaveScaling(int wave)
        {
            ApplyWaveScaling(wave, 0.25f, 0.05f);
        }

        public void ApplyWaveScaling(int wave, float hpGrowth, float speedGrowth)
        {
            int scaledHP = Mathf.RoundToInt(baseHP * (1f + wave * hpGrowth));
            float scaledSpeed = baseSpeed * (1f + wave * speedGrowth);

            // EnemyHealth에 스케일링된 HP 적용
            var health = GetComponent<EnemyHealth>();
            if (health != null)
            {
                // EnemyHealth에 SetMaxHP가 있다면, 아래 '방법 2' 참고
                health.SetMaxHP(scaledHP);
            }

            // EnemyMovement에 스케일링된 속도 적용
            var movement = GetComponent<EnemyMovement>();
            if (movement != null)
            {
                // EnemyMovement에 SetSpeed가 있다면, 아래 '방법 2' 참고
                movement.SetSpeed(scaledSpeed);
            }
        }
    }
}
