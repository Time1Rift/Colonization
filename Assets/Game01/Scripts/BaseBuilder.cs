using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BaseBuilder : MonoBehaviour
{
    [SerializeField] private Material _materialPrefab;
    [SerializeField] private Flag _flagPrefab;
    [SerializeField] private Base _basePrefab;

    private Material _startMaterial;
    private MeshRenderer _renderer;
    private bool isFlag = false;
    private Flag _newFlag;

    public event Action BaseBeganBuilt;
    public event Action BaseBuilt;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _startMaterial = _renderer.material;
    }

    public void CreateBase(Minion minion)
    {
        minion.CreateBase(_newFlag);
        _renderer.material = _startMaterial;
        BaseBuilt?.Invoke();
    }

    public void PutUpFlag(Vector3 position)
    {
        if (isFlag)
        {
            _newFlag = Instantiate(_flagPrefab, position, Quaternion.identity);
            isFlag = false;
            BaseBeganBuilt?.Invoke();
        }
        else
        {
            _newFlag.transform.position = position;
        }
    }

    public void Work()
    {
        _renderer.material = _materialPrefab;
        isFlag = true;
    }
}