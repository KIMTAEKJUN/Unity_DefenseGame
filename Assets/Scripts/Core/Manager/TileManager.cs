using System.Collections.Generic;
using Pattern;
using UnityEngine;

namespace Manager
{
    public class TileManager : Singleton<TileManager>
    {
        [SerializeField] private List<Color> waveColors;
        [SerializeField] private List<Tile> tiles;

        private void Start()
        {
            WaveManager.Instance.OnWaveEnd += ChangeWaveColor;
        }

        private void OnDestroy()
        {
            WaveManager.Instance.OnWaveEnd -= ChangeWaveColor;
        }

        private void ChangeWaveColor(int waveIndex)
        {
            if (waveIndex < waveColors.Count)
            {
                Color newColor = waveColors[waveIndex];
                foreach (var tile in tiles)
                {
                    tile.ChangeColor(newColor);
                }
            }
        }
    }
}