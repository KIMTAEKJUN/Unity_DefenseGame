using Pattern;
using UnityEngine;

namespace Manager
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        // 기본값만 설장하기에 멤버 이니셜라이저 사용
        public float Score { get; set; } = 0;

        public void AddScore(float amount)
        {
            Score += amount;
            Debug.Log($"점수 {amount}를 획득했습니다. 현재 점수: {Score}");
        }
    }
}
