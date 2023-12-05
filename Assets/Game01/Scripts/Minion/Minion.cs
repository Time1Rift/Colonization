using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(MinionMover))]
[RequireComponent(typeof(MinionCollector))]
[RequireComponent(typeof(MinionBuilder))]
public class Minion : MonoBehaviour
{
    private MinionMover _minionMover;
    private MinionCollector _minionCollector;
    private MinionBuilder _minionBuilder;
    private Vector3 _targetBase;

    public bool IsFree { get; private set; } = true;

    private void OnDisable()
    {
        _minionCollector.ResourceCollected -= AssignResourceBase;
        _minionBuilder.Free -= GetFree;
    }

    public void CreateBase(Flag flag)
    {
        IsFree = false;
        _minionBuilder.SetTargetFlag(flag);
        _minionMover.SetTargetPosition(flag.transform.position);
    }    

    public void SubmitResource(Resource resource)
    {
        Destroy(resource.gameObject);
        IsFree = true;
    }

    public void GoAfterResource(Resource resource)
    {
        IsFree = false;
        _minionCollector.SetTargetResource(resource);
        _minionMover.SetTargetPosition(resource.transform.position);
    }

    public void SetTargetPositionBase()
    {
        _targetBase = transform.GetComponentInParent<Base>().transform.position;
        _minionMover = GetComponent<MinionMover>();
        _minionCollector = GetComponent<MinionCollector>();
        _minionBuilder = GetComponent<MinionBuilder>();

        _minionCollector.ResourceCollected += AssignResourceBase;
        _minionBuilder.Free += GetFree;
    }

    private void GetFree()
    {
        SetTargetPositionBase();
        IsFree = true;
    }

    private void AssignResourceBase()
    {
        _minionMover.SetTargetPosition(_targetBase);
    }
}