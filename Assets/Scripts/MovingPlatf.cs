using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatf : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _targetToGo = new List<Transform>();
    private Transform _destination;
    [SerializeField]
    private float _platfSpeed;

    private bool _inverse;
    private int _targetIndex = 1;

    private void Start()
    {
        if (_targetToGo != null)
            transform.position = _targetToGo[0].position;

        _destination = _targetToGo[1];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlatfMouvements();
    }

    void PlatfMouvements()
    {
        if (transform.position == _destination.position)
        {
            if (_inverse == false)
                _targetIndex++;
            else
                _targetIndex--;

            if (_targetIndex == _targetToGo.Count)
            {
                _inverse = true;
                _targetIndex -= 2;
            }
            else if (_targetIndex < 0)
            {
                _inverse = false;
                _targetIndex += 2;
            }

            _destination = _targetToGo[_targetIndex];
        }
        transform.position = Vector3.MoveTowards(transform.position, _destination.position, Time.deltaTime * _platfSpeed);
    }
}
