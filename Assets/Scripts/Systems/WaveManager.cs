using UnityEngine;
using TMPro;
using System.Collections;

namespace Systems
{
    public class WaveManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private EnemySpawner spawner;
        [SerializeField] private WaveConfig config;
        [SerializeField] private TMP_Text waveText;

        [Header("Wave Timing")]
        [SerializeField] private float baseWaveDuration = 15f;

        [Header("Option C: Faster after threshold")]
        [SerializeField] private int speedUpStartWave = 10;     // 이 웨이브부터
        [SerializeField] private float fastWaveDuration = 10f;  // 웨이브 시간을 25초로

        public int CurrentWave { get; private set; } = 0;

        private float waveTimer;
        private Coroutine spawnCoroutine;

        private void Start()
        {
            StartNextWave();
        }

        private void Update()
        {
            if (CurrentWave <= 0) return;

            waveTimer -= Time.deltaTime;
            if (waveTimer <= 0f)
            {
                StartNextWave(); // 적 남아 있어도 다음 웨이브로 넘어감
            }
        }

        public void StartNextWave()
        {
            // 웨이브 증가
            CurrentWave++;

            // 웨이브 시간(옵션 C 적용)
            float duration = (CurrentWave >= speedUpStartWave) ? fastWaveDuration : baseWaveDuration;
            waveTimer = duration;

            // UI 업데이트
            if (waveText != null)
                waveText.text = $"Wave {CurrentWave}";

            // 이전 웨이브의 "스폰"만 종료(적 오브젝트는 남겨둠)
            if (spawnCoroutine != null)
                StopCoroutine(spawnCoroutine);

            // 이번 웨이브 스폰 시작 (시간 기반 스폰)
            float interval = config.GetInterval(CurrentWave);
            spawnCoroutine = StartCoroutine(SpawnForDuration(duration, interval));
        }

        private IEnumerator SpawnForDuration(float duration, float interval)
        {
            float t = 0f;

            while (t < duration)
            {
                // 스폰 함수에 맞춰 호출
                spawner.SpawnScaledEnemy(CurrentWave);  // 만약 더 있으면 스폰 시그니처수정

                yield return new WaitForSeconds(interval);
                t += interval;
            }
        }
    }
}

