using UnityEngine;
using Manager;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField] private TowerManager towerManager;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
        if (_mainCamera == null)
        {
            Debug.LogError("Main Camera not found!");
        }

        if (towerManager == null)
        {
            Debug.LogError("TowerManager reference is missing!");
        }
    }

    private void Update()
    {
        if (_mainCamera == null || towerManager == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider != null && hit.collider.CompareTag("Tile"))
                {
                    towerManager.BuildTower(hit.transform);
                }
            }
        }
    }
}