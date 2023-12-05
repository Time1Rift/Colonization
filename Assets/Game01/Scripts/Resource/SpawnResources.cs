using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class SpawnResources : MonoBehaviour
{
    [SerializeField] private Resource _resourcePrefab;
    [SerializeField] private float _timeBetweenSpawn;

    private Coroutine _createResource;
    private BoxCollider _boxCollider;
    private Scanner _scanner;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _scanner = transform.GetComponentInParent<Scanner>();
        _createResource = StartCoroutine(CreateResource());
    }

    private void OnDisable()
    {
        if (_createResource != null)
            StopCoroutine(_createResource);
    }

    private IEnumerator CreateResource()
    {
        var interval = new WaitForSecondsRealtime(_timeBetweenSpawn);
        float height = 0.3f;

        while (enabled)
        {
            Vector3 position = new Vector3(Random.Range(0, _boxCollider.size.x), height, Random.Range(0, _boxCollider.size.z));
            Resource resource = Instantiate(_resourcePrefab, position, Quaternion.identity);
            _scanner.AddResources(resource);
            yield return interval;
        }
    }
}