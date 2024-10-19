using Pattern;

namespace Manager
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private Button startWaveButton;
        [SerializeField] private WaveManager waveManager;

        private void Start()
        {
            if (startWaveButton != null && waveManager != null)
            {
                startWaveButton.onClick.AddListener(OnStartWaveButtonClick);
                waveManager.OnWaveStart += OnWaveStart;
                waveManager.OnAllWavesCompleted += OnAllWavesCompleted;
            }
            else
            {
                Debug.LogError("StartWave button or WaveManager is not assigned in UIManager");
            }
        }

        private void OnStartWaveButtonClick()
        {
            if (!waveManager.IsWaveInProgress)
            {
                waveManager.StartWave();
            }
        }

        private void OnWaveStart(int waveIndex)
        {
            startWaveButton.interactable = false;
        }

        private void OnAllWavesCompleted()
        {
            startWaveButton.interactable = false;
            // 여기에 게임 종료 또는 재시작 로직을 추가할 수 있습니다.
        }

        private void OnDestroy()
        {
            if (startWaveButton != null) startWaveButton.onClick.RemoveListener(OnStartWaveButtonClick);
            if (waveManager != null)
            {
                waveManager.OnWaveStart -= OnWaveStart;
                waveManager.OnAllWavesCompleted -= OnAllWavesCompleted;
            }
        }
    }
}