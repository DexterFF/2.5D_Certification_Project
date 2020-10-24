using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _levels = new List<Transform>();
    private Transform _destination;
    [SerializeField]
    private float _elevatorSpeed;
    [SerializeField]
    private float _pauseDuration;
    private WaitForSeconds _delay;

    private bool _inverse;
    private int _levelIndex = 1;

    private bool _pauseElevator;

    private void Start()
    {
        _delay = new WaitForSeconds(_pauseDuration);
        if(_levels != null)
            transform.position = _levels[0].position;

        _destination = _levels[1];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_pauseElevator == false)
            ElevatorMouvements();
    }

    void ElevatorMouvements()
    {
        if (transform.position == _destination.position)
        {
            if (_inverse == false)
                _levelIndex++;
            else
                _levelIndex--;

            if (_levelIndex == _levels.Count)
            {
                _inverse = true;
                _levelIndex -= 2;
            }
            else if (_levelIndex < 0)
            {
                _inverse = false;
                _levelIndex += 2;
            }

            _destination = _levels[_levelIndex];
            StartCoroutine(PauseDelay());
        }
        transform.position = Vector3.MoveTowards(transform.position, _destination.position, Time.deltaTime * _elevatorSpeed);
    }

    IEnumerator PauseDelay()
    {
        _pauseElevator = true;
        yield return _delay;
        _pauseElevator = false;
    }
}
