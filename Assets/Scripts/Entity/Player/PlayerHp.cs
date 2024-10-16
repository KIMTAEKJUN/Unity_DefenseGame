using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace Player
{
    // 간단히 옵저버 패턴을 사용해서 플레이어의 체력을 관리하는 클래스
    public class PlayerHp : MonoBehaviour
    {
        [SerializeField] private Image imageScreen;
        [SerializeField] private float maxHp = 20;
        private float _currentHp;

        // 체력이 변경될 때 호출할 이벤트
        public event Action<float> OnHpChanged;
        
        // 플레이어가 죽었을 때 호출할 이벤트
        public event Action OnPlayerDeath;
        
        public float MaxHp => maxHp;
        public float CurrentHp
        {
            get => _currentHp;
            private set
            {
                _currentHp = Mathf.Clamp(value, 0, maxHp);
                OnHpChanged?.Invoke(_currentHp);
                if (_currentHp <= 0)
                {
                    OnPlayerDeath?.Invoke();
                }
            }
        }
        
        private void Awake()
        {
            CurrentHp = maxHp;
        }
        
        public void TakeDamage(float damage)
        {
            CurrentHp -= damage;
            
            StopCoroutine(HitAlphaAnimation());
            StartCoroutine(HitAlphaAnimation());
            
            if (CurrentHp <= 0)
            {
                Debug.Log("Player Die");
                OnPlayerDeath?.Invoke();
            }
        }
        
        private IEnumerator HitAlphaAnimation()
        {
            Color color = imageScreen.color;
            color.a = 0.4f;
            imageScreen.color = color;

            while (color.a >= 0.0f)
            {
                color.a -= Time.deltaTime;
                imageScreen.color = color;

                yield return null;
            }
        }
    }
}