using UnityEngine;

namespace Systems
{
    public enum UpgradeType { FireRate, Damage, Range }

    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField] private int startingGold = 100;
        public int Gold { get; private set; }

        public int FireRateLevel { get; private set; }
        public int DamageLevel { get; private set; }
        public int RangeLevel { get; private set; }

        [SerializeField] private BunkerGun bunkerGun;

        private void Awake()
        {
            Gold = startingGold;

            if (bunkerGun == null)
                bunkerGun = FindObjectOfType<BunkerGun>();
        }

        public int GetCost(UpgradeType type)
        {
            int level = type switch
            {
                UpgradeType.FireRate => FireRateLevel,
                UpgradeType.Damage => DamageLevel,
                UpgradeType.Range => RangeLevel,
                _ => 0
            };

            int baseCost = type switch
            {
                UpgradeType.FireRate => 50,
                UpgradeType.Damage => 40,
                UpgradeType.Range => 30,
                _ => 50
            };

            return Mathf.RoundToInt(baseCost * Mathf.Pow(1.6f, level));
        }

        public bool TryUpgrade(UpgradeType type)
        {
            if (bunkerGun == null) return false;

            int cost = GetCost(type);
            if (Gold < cost) return false;

            Gold -= cost;

            switch (type)
            {
                case UpgradeType.FireRate:
                    FireRateLevel++;
                    bunkerGun.ApplyUpgrade(0.92f, 0, 0f);
                    break;

                case UpgradeType.Damage:
                    DamageLevel++;
                    bunkerGun.ApplyUpgrade(1f, 3, 0f);
                    break;

                case UpgradeType.Range:
                    RangeLevel++;
                    bunkerGun.ApplyUpgrade(1f, 0, 0.6f);
                    break;
            }

            return true;
        }

        public void AddGold(int amount)
        {
            Gold += Mathf.Max(0, amount);
        }
    }
}
