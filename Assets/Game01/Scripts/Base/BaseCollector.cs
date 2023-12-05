using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollector : MonoBehaviour
{
    public event Action Collected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Minion minion))
        {
            Resource resource = minion.GetComponentInChildren<Resource>();

            if (resource != null)
            {
                minion.SubmitResource(resource);
                Collected?.Invoke();
            }            
        }
    }
}