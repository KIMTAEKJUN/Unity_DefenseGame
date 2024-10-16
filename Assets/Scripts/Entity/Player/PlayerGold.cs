using UnityEngine;
using System;

namespace Player
{
    // 간단히 옵저버 패턴을 사용해서 플레이어의 골드를 관리하는 클래스
    public class PlayerGold : MonoBehaviour
    {
        [SerializeField]
        private int currentGold = 100;

        // 골드가 변경될 때 호출할 이벤트
        public event Action<int> OnGoldChanged;
        
        public int CurrentGold
        {
            get => currentGold;
            set 
            {
                int newValue = Mathf.Max(0, value);
                if (currentGold != newValue)
                {
                    currentGold = newValue;
                    OnGoldChanged?.Invoke(currentGold);
                }
            }
        }
    }
}