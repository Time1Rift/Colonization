using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCreatedMinion : MonoBehaviour
{
    [SerializeField] private Minion _minionPrefab;

    public Minion Create()
    {
        Minion minion = Instantiate(_minionPrefab, transform.position, Quaternion.identity);
        minion.transform.parent = transform;
        minion.SetTargetPositionBase();
        return minion;
    }
}