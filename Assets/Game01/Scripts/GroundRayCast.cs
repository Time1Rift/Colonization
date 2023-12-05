using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRayCast : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private List<Transform> _bases = new List<Transform>();
    private int _index = 0;
    private bool _isBase = false;
    private BaseBuilder _baseBuilder;

    private void Awake()
    {
        AddBase();
    }

    private void OnDisable()
    {
        _baseBuilder.BaseBuilt -= FinishPositioning;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SetTargetPositionBuilding();
    }

    private void FinishPositioning()
    {
        _isBase = false;
        _baseBuilder = null;
        AddBase();        
    }

    private void AddBase()
    {
        for (int i = 1; i < transform.childCount; i++)
            _bases.Add(transform.GetChild(i).transform);
    }

    private void SetTargetPositionBuilding()
    {
        int exception = -1;
        int constructionThreshold = 1;

        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            _index = _bases.IndexOf(hit.transform);

            if (_index != exception && _isBase == false && _bases[_index].childCount > constructionThreshold)
            {
                _isBase = true;
                _index = _bases.IndexOf(hit.transform);
                _baseBuilder = _bases[_index].GetComponent<BaseBuilder>();
                _baseBuilder.BaseBuilt += FinishPositioning;
                _baseBuilder.Work();
                _index = 0;
            }

            if (hit.transform.GetComponent<SpawnResources>() && _isBase)
            {
                _baseBuilder.PutUpFlag(hit.point);
            }
        }
    }
}