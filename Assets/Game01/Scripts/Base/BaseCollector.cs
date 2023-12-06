using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Base))]
public class BaseCollector : MonoBehaviour
{
    private Base _base;

    public event Action Collected;

    private void Awake()
    {
        _base = GetComponent<Base>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Minion minion))
        {
            Resource resource = minion.GetComponentInChildren<Resource>();

            if (resource != null)
            {
                minion.SubmitResource(resource);
                _base.AddMinion(minion);
                Collected?.Invoke();
            }            
        }
    }
}