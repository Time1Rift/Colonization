using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private Queue<Resource> _resources = new Queue<Resource>();

    public void AddResources(Resource resource)
    {
        _resources.Enqueue(resource);
    }

    public bool TryHereResources() => _resources.Count > 0;

    public Resource GetResource()
    {
        Resource resource = _resources.Dequeue();

        while (resource == null)
            resource = _resources.Dequeue();

        return resource;
    }
}