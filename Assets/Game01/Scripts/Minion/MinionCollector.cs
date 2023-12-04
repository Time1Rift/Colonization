using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionCollector : MonoBehaviour
{
    private Resource _resource;

    public event Action ResourceCollected;

    private void OnTriggerEnter(Collider other)
    {
        float height = 0.5f;

        if (other.TryGetComponent(out Resource resource) && _resource == resource)
        {
            resource.transform.SetParent(transform);
            transform.position = new Vector3(transform.position.x, height, transform.position.z);
            ResourceCollected?.Invoke();
        }
    }

    public void SetTargetResource(Resource resource)
    {
        _resource = resource;
    }
}