using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseCreatedMinion))]
[RequireComponent(typeof(BaseWallet))]
[RequireComponent(typeof(BaseBuilder))]
public class Base : MonoBehaviour
{
    private Scanner _scanner;
    private Queue<Minion> _minions = new Queue<Minion>();
    private BaseCreatedMinion _baseCreatedMinion;
    private BaseWallet _baseWallet;
    private BaseBuilder _baseBuilder;
    private bool _isBaseBuilding = false;

    private void Awake()
    {
        _baseCreatedMinion = GetComponent<BaseCreatedMinion>();
        _baseWallet = GetComponent<BaseWallet>();
        _baseBuilder = GetComponent<BaseBuilder>();
        FillFields();
        CreateMinion();
    }

    private void OnEnable()
    {
        _baseWallet.MinionCreated += CreateMinion;
        _baseWallet.BaseCreated += CreateBase;
    }

    private void OnDisable()
    {
        _baseWallet.MinionCreated -= CreateMinion;
        _baseWallet.BaseCreated -= CreateBase;
    }

    private void Update()
    {
        while (_minions.Count > 0 && _scanner.TryHereResources())
        {
            if (_isBaseBuilding)
            {
                Minion minion = _minions.Dequeue();
                Build(minion);
                _isBaseBuilding = false;
            }
            else
            {
                Minion minion = _minions.Dequeue();
                minion.GoAfterResource(_scanner.GetResource());
            }
        }
    }

    public void AddMinion(Minion minion)
    {
        _minions.Enqueue(minion);
    }

    public void ClearMinions()
    {
        foreach (var minion in _minions)
            Destroy(minion.gameObject);
    }

    public void FillFields()
    {
        _scanner = transform.GetComponentInParent<Scanner>();
    }

    private void Build(Minion minion)
    {
        _baseBuilder.CreateBase(minion);
    }

    private void CreateBase()
    {
        _isBaseBuilding = true;
    }

    private void CreateMinion()
    {
        _minions.Enqueue(_baseCreatedMinion.Create());
    }
}