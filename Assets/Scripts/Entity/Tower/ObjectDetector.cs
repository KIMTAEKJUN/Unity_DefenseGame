using System;
using Manager;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private TowerManager _towerManager;
    private Camera _mainCamera;
    private Ray _ray;
    private RaycastHit _hit;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity))
            {
                if (_hit.collider.CompareTag("Tile"))
                {
                    _towerManager.BuildTower(_hit.transform);
                }
            }
        }
    }
}