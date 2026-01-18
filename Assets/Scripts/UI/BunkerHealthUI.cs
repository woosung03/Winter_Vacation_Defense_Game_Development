using UnityEngine;
using UnityEngine.UI;
using Entities;
using TMPro;

namespace UI
{
    public class BunkerHealthUI : MonoBehaviour
    {
        [Header("Target")]
        [SerializeField] private BunkerHealth bunkerHealth;

        [Header("UI")]
        [SerializeField] private Slider hpSlider;
        [SerializeField] private TMP_Text hpText; // 없어도 됨(비워도 동작)

        private void Awake()
        {
            if (hpSlider != null)
            {
                hpSlider.minValue = 0f;
                hpSlider.maxValue = 1f;
            }

            // 타겟 자동 찾기(원하면 직접 드래그해도 됨)
            if (bunkerHealth == null)
            {
                var bunker = GameObject.FindGameObjectWithTag("Bunker");
                if (bunker != null) bunkerHealth = bunker.GetComponent<BunkerHealth>();
            }
        }

        private void OnEnable()
        {
            if (bunkerHealth != null)
                bunkerHealth.OnHealthChanged += HandleHealthChanged;
        }

        private void OnDisable()
        {
            if (bunkerHealth != null)
                bunkerHealth.OnHealthChanged -= HandleHealthChanged;
        }

        private void Start()
        {
            // 시작 시 즉시 한번 갱신
            if (bunkerHealth != null)
                HandleHealthChanged(bunkerHealth.CurrentHealth, bunkerHealth.MaxHealth);
        }

        private void HandleHealthChanged(int current, int max)
        {
            float normalized = (max <= 0) ? 0f : (float)current / max;

            if (hpSlider != null)
                hpSlider.value = normalized;

            if (hpText != null)
                hpText.text = $"{current} / {max}";
        }
    }
}
