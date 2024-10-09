using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LevelSelect : MonoBehaviour
    {
        public void LoadLevel(int level)
        {
            PlayerPrefs.SetInt("SelectedLevel", level);  // 선택한 레벨 저장
            SceneManager.LoadScene("GameScene");  // 게임 씬 로드
        }
    }
}