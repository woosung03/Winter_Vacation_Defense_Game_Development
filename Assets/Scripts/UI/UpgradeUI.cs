using UnityEngine;
using TMPro;
using Systems;

namespace UI
{
    public class UpgradeUI : MonoBehaviour
    {
        [SerializeField] private UpgradeManager upgradeManager;

        [Header("Texts")]
        [SerializeField] private TMP_Text goldText;
        [SerializeField] private TMP_Text fireRateText;
        [SerializeField] private TMP_Text damageText;
        [SerializeField] private TMP_Text rangeText;

        private void Awake()
        {
            if (upgradeManager == null)
                upgradeManager = FindObjectOfType<UpgradeManager>();
        }

        private void Update()
        {
            if (upgradeManager == null) return;

            if (goldText != null)
                goldText.text = $"Gold: {upgradeManager.Gold}";

            if (fireRateText != null)
                fireRateText.text =
                    $"FireRate Lv.{upgradeManager.FireRateLevel} (Cost {upgradeManager.GetCost(UpgradeType.FireRate)})";

            if (damageText != null)
                damageText.text =
                    $"Damage Lv.{upgradeManager.DamageLevel} (Cost {upgradeManager.GetCost(UpgradeType.Damage)})";

            if (rangeText != null)
                rangeText.text =
                    $"Range Lv.{upgradeManager.RangeLevel} (Cost {upgradeManager.GetCost(UpgradeType.Range)})";
        }

        // 버튼 OnClick 연결용
        public void UpgradeFireRate()
        {
            if (upgradeManager != null)
                upgradeManager.TryUpgrade(UpgradeType.FireRate);
        }

        public void UpgradeDamage()
        {
            if (upgradeManager != null)
                upgradeManager.TryUpgrade(UpgradeType.Damage);
        }

        public void UpgradeRange()
        {
            if (upgradeManager != null)
                upgradeManager.TryUpgrade(UpgradeType.Range);
        }
    }
}
