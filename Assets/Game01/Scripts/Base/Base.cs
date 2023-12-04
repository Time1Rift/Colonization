using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    private List<Minion> _minions = new List<Minion>();
    private Scanner _scanner;
    private BaseCreatedMinion _baseCreatedMinion;
    private BaseWallet _baseWallet;

    private void Awake()
    {
        _baseCreatedMinion = GetComponent<BaseCreatedMinion>();
        _baseWallet = GetComponent<BaseWallet>();
        _scanner = transform.GetComponentInParent<Scanner>();

        
        CreateMinion();
    }

    private void OnEnable()
    {
        _baseWallet.MinionCreated += CreateMinion;
    }

    private void OnDisable()
    {
        _baseWallet.MinionCreated -= CreateMinion;
    }

    private void Update()
    {
        for (int i = 0; i < _minions.Count; i++)
        {
            if (_minions[i].IsFree && _scanner.TryHereResources())
                _minions[i].GoAfterResource(_scanner.GetResource());
        }
    }

    private void CreateMinion()
    {
        _minions.Add(_baseCreatedMinion.Create());
    }
}