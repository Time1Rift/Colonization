using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseCollector))]
[RequireComponent(typeof(BaseBuilder))]
public class BaseWallet : MonoBehaviour
{
    private int _countPoints;
    private int _countPointsCreateMinion = 3;
    private int _countPointsCreateBase = 2;
    private bool _inBaseBeingBuilt = false;

    private BaseCollector _baseResourceCollector;
    private BaseBuilder _baseBuilder;

    public event Action MinionCreated;
    public event Action BaseCreated;

    private void Awake()
    {
        _baseResourceCollector = GetComponent<BaseCollector>();
        _baseBuilder = GetComponent<BaseBuilder>();
    }

    private void OnEnable()
    {
        _baseResourceCollector.Collected += AddPoint;
        _baseBuilder.BaseBeganBuilt += Build;
    }

    private void OnDisable()
    {
        _baseResourceCollector.Collected -= AddPoint;
        _baseBuilder.BaseBeganBuilt -= Build;
    }

    private void Build()
    {
        _inBaseBeingBuilt = true;
    }

    private void CanCreateMinion()
    {
        if (_countPoints >= _countPointsCreateMinion && _inBaseBeingBuilt == false)
        {
            _countPoints -= _countPointsCreateMinion;
            Debug.Log("Был создан Миньон, осталось Монет  -  " + _countPoints);
            MinionCreated?.Invoke();
        }
    }

    private void CanCreateBase()
    {
        if (_countPoints >= _countPointsCreateBase && _inBaseBeingBuilt)
        {
            _countPoints -= _countPointsCreateBase;
            Debug.Log("Была создана База, осталось Монет  -  " + _countPoints);
            _inBaseBeingBuilt = false;
            BaseCreated?.Invoke();
        }
    }

    private void AddPoint()
    {
        _countPoints++;
        Debug.Log(" Монет  -  " + _countPoints);

        CanCreateMinion();
        CanCreateBase();
    }
}