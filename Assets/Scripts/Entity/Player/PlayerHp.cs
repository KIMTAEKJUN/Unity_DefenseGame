using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Player
{
    public class PlayerHp : MonoBehaviour
    {
        [SerializeField] private Image imageScreen;
        [SerializeField] private float maxHp = 20;
        private float _currentHp;
        
        public float MaxHp => maxHp;
        public float CurrentHp => _currentHp;
        
        private void Awake()
        {
            _currentHp = maxHp;
        }
        
        public void TakeDamage(float damage)
        {
            _currentHp -= damage;
            
            StopCoroutine(HitAlphaAnimation());
            StartCoroutine(HitAlphaAnimation());
            
            if (_currentHp <= 0)
            {
                Debug.Log("Player Die");
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