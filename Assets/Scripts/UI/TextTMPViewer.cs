using Manager;
using Player;
using TMPro;
using UnityEngine;
using System.Collections;

namespace UI
{
    // 간단하게 옵저버 패턴을 사용해서 텍스트를 업데이트하는 클래스
    public class TextTMPViewer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textPlayerHp;
        [SerializeField] private PlayerHp playerHp;
        [SerializeField] private TextMeshProUGUI textPlayerGold;
        [SerializeField] private PlayerGold playerGold;
        [SerializeField] private TextMeshProUGUI textWave;
        [SerializeField] private WaveManager waveManager;
        [SerializeField] private TextMeshProUGUI textEnemyCount;
        [SerializeField] private EnemyManager enemyManager;

        // 주기적 업데이트 간격
        [SerializeField] private float updateInterval = 0.5f;

        private void Start()
        {
            // PlayerHp와 PlayerGold의 이벤트 구독
            playerHp.OnHpChanged += UpdatePlayerHpText;
            playerGold.OnGoldChanged += UpdatePlayerGoldText;

            // 초기 텍스트 설정
            UpdatePlayerHpText(playerHp.CurrentHp);
            UpdatePlayerGoldText(playerGold.CurrentGold);
            UpdateWaveText();
            UpdateEnemyCountText();

            // 주기적으로 웨이브와 적 수 업데이트
            StartCoroutine(UpdateWaveAndEnemyCount());
        }
        
        private void OnDestroy()
        {
            // 이벤트 구독 해제
            playerHp.OnHpChanged -= UpdatePlayerHpText;
            playerGold.OnGoldChanged -= UpdatePlayerGoldText;
        }

        // 플레이어 체력 업데이트
        private void UpdatePlayerHpText(float currentHp)
        {
            textPlayerHp.text = $"{currentHp}/{playerHp.MaxHp}";
        }

        // 골드 업데이트
        private void UpdatePlayerGoldText(int currentGold)
        {
            textPlayerGold.text = currentGold.ToString();
        }

        // 웨이브 업데이트
        private void UpdateWaveText()
        {
            textWave.text = $"{waveManager.CurrentWave}/{waveManager.MaxWave}";
        }

        // 적 수 업데이트
        private void UpdateEnemyCountText()
        {
            textEnemyCount.text = $"{enemyManager.CurrentEnemyCount}/{enemyManager.MaxEnemyCount}";
        }

        // 주기적으로 웨이브와 적 수 업데이트
        private IEnumerator UpdateWaveAndEnemyCount()
        {
            while (true)
            {
                UpdateWaveText();
                UpdateEnemyCountText();
                yield return new WaitForSeconds(updateInterval);
            }
        }
    }
}