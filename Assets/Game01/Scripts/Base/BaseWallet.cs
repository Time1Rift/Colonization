using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseWallet : MonoBehaviour
{
    private int _countPoints;
    private int _countPointsCreateMinion = 3;
    //private int _countPointsCreateBase = 5;
    
    private BaseResourceCollector _baseResourceCollector;

    public event Action MinionCreated;
    //public event Action BaseCreated;

    private void Awake()
    {
        _baseResourceCollector = GetComponent<BaseResourceCollector>();
    }

    private void OnEnable()
    {
        _baseResourceCollector.Collected += AddPoint;
    }

    private void OnDisable()
    {
        _baseResourceCollector.Collected -= AddPoint;
    }

    private void CanCreateMinion()
    {
        if (_countPoints >= _countPointsCreateMinion)
        {
            _countPoints -= _countPointsCreateMinion;
            MinionCreated?.Invoke();
        }
    }

    //private void CanCreateBase()
    //{
    //    if (_countPoints >= _countPointsCreateBase)
    //    {
    //        _countPoints -= _countPointsCreateBase;
    //        BaseCreated?.Invoke();
    //    }
    //}

    private void AddPoint()
    {
        _countPoints++;
        Debug.Log(" Монет  -  " + _countPoints);

        CanCreateMinion();
        //CanCreateBase();
    }
}