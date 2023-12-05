using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBuilder : MonoBehaviour
{
    [SerializeField] private Base _basePrefab;

    private Flag _flag;

    public event Action Free;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Flag>(out Flag flag) && _flag == flag)
        {
            Destroy(flag.gameObject);
            _flag = null;

            Base newBase = Instantiate(_basePrefab, transform.position, Quaternion.identity);
            newBase.ClearMinions();
            newBase.transform.parent = transform.GetComponentInParent<Base>().GetComponentInParent<Scanner>().transform;
            newBase.FillFields();

            transform.parent = newBase.transform;
            newBase.AddMinion(transform.GetComponent<Minion>());

            Free?.Invoke();
        }
    }

    public void SetTargetFlag(Flag flag)
    {
        _flag = flag;
    }
}