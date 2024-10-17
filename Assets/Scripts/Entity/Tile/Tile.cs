using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsBuildTower { get; set; }
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        IsBuildTower = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeColor(Color newColor)
    {
        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = newColor;
        }
    }
}