using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Entity.Tower
{
    namespace Entity.Tower
    {
        public class TowerSelectionUI : MonoBehaviour
        {
            [SerializeField] private Button[] towerButtons;

            private void Start()
            {
                for (int i = 0; i < towerButtons.Length; i++)
                {
                    int index = i;
                    towerButtons[i].onClick.AddListener(() => OnTowerButtonClick(index));
                }

                if (TowerManager.Instance != null)
                {
                    TowerManager.Instance.OnTowerSelected += HighlightSelectedTower;
                }
            }

            private void OnTowerButtonClick(int index)
            {
                if (TowerManager.Instance != null)
                {
                    TowerManager.Instance.SelectTower(index);
                }
            }

            private void HighlightSelectedTower(int index)
            {
                for (int i = 0; i < towerButtons.Length; i++)
                {
                    towerButtons[i].GetComponent<Image>().color = i == index ? Color.yellow : Color.white;
                }
            }

            private void OnDestroy()
            {
                if (TowerManager.Instance != null)
                {
                    TowerManager.Instance.OnTowerSelected -= HighlightSelectedTower;
                }
            }
        }
    }
}