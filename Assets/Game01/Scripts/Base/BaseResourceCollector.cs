using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseResourceCollector : MonoBehaviour
{
    public event Action Collected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Minion minion))
        {
            if (minion.transform.childCount > 0)
            {
                minion.SubmitResource();
                Collected?.Invoke();
            }            
        }
    }
}