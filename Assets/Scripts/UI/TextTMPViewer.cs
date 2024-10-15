using Manager;
using Map;
using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TextTMPViewer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textPlayerHp;
        [SerializeField] private PlayerHp playerHp;
        [SerializeField] private TextMeshProUGUI textPlayerGold;
        [SerializeField] private PlayerGold playerGold;
        [SerializeField] private TextMeshProUGUI textWave;
        [SerializeField] private WaveSystem waveSystem;
        [SerializeField] private TextMeshProUGUI textEnemyCount;
        [SerializeField] private EnemyManager enemyManager;

        private void Update()
        {
            textPlayerHp.text = playerHp.CurrentHp + "/" + playerHp.MaxHp;
            textPlayerGold.text = playerGold.CurrentGold.ToString();
            textWave.text = waveSystem.CurrentWave + "/" + waveSystem.MaxWave;
            textEnemyCount.text = enemyManager.CurrentEnemyCount + "/" + enemyManager.MaxEnemyCount;
        }
    }
}