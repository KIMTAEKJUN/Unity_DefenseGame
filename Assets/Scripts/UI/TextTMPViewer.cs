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

        private void Update()
        {
            textPlayerHp.text = playerHp.CurrentHp + "/" + playerHp.MaxHp;
            textPlayerGold.text = playerGold.CurrentGold.ToString();
        }
    }
}