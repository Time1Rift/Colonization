using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(MinionMover)), RequireComponent(typeof(MinionCollector))]
public class Minion : MonoBehaviour
{
    private MinionMover _minionMover;
    private MinionCollector _minionCollector;
    private Vector3 _targetBase;

    public bool IsFree { get; private set; }

    private void Awake()
    {
        IsFree = true;
        _targetBase = transform.GetComponentInParent<Base>().transform.position;
        _minionMover = GetComponent<MinionMover>();
        _minionCollector = GetComponent<MinionCollector>();
    }

    public void OnEnable()
    {
        _minionCollector.ResourceCollected += AssignResourceBase;
    }

    public void OnDisable()
    {
        _minionCollector.ResourceCollected -= AssignResourceBase;
    }

    public void SubmitResource()
    {
        Destroy(transform.GetChild(0).gameObject);
        IsFree = true;
    }

    public void GoAfterResource(Resource resource)
    {
        IsFree = false;
        _minionCollector.SetTargetResource(resource);
        _minionMover.SetTargetPosition(resource.transform.position);
    }

    private void AssignResourceBase()
    {
        _minionMover.SetTargetPosition(_targetBase);
    }
}