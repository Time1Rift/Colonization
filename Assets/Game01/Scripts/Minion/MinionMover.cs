using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private bool _haveTarget = false;
    private Vector3 _targetPosition;

    private void Update()
    {
        if (_haveTarget)
        {
            transform.forward = _targetPosition - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);

            if (transform.position == _targetPosition)
                _haveTarget = false;
        }
    }

    public void SetTargetPosition(Vector3 position)
    {
        _targetPosition = position;
        _haveTarget = true;
    }
}