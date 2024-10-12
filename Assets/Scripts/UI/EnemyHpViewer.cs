using UnityEngine.UI;
using UnityEngine;

namespace UI
{
    public class EnemyHpViewer : MonoBehaviour
    {
        private EnemyHp _enemyHp;
        private Slider _slider;
        
        public void Setup(EnemyHp enemyHp)
        {
            _enemyHp = enemyHp;
            _slider = GetComponent<Slider>();
        }

        private void Update()
        {
            _slider.value = _enemyHp.CurrentHp / _enemyHp.MaxHp;
        }
    }
}