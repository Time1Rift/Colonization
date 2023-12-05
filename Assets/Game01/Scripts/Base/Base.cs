using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseCreatedMinion))]
[RequireComponent(typeof(BaseWallet))]
[RequireComponent(typeof(BaseBuilder))]
public class Base : MonoBehaviour
{
    private Scanner _scanner;
    private List<Minion> _minions = new List<Minion>();
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
        for (int i = 0; i < _minions.Count; i++)
        {
            if (_minions[i].IsFree && _scanner.TryHereResources())
            {
                if (_isBaseBuilding)
                {
                    Minion minion = _minions[i];
                    _minions.Remove(_minions[i]);
                    Build(minion);
                    _isBaseBuilding = false;
                }
                else
                {
                    _minions[i].GoAfterResource(_scanner.GetResource());
                }
            }
        }
    }

    public void AddMinion(Minion minion)
    {
        _minions.Add(minion);
    }

    public void ClearMinions()
    {
        for (int i = 0; i < _minions.Count; i++)
        {
            Destroy(_minions[i].gameObject);
        }
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
        _minions.Add(_baseCreatedMinion.Create());
    }
}