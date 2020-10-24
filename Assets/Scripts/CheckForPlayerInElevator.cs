using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForPlayerInElevator : MonoBehaviour
{
    private Transform _elevator;

    private void Start()
    {
        _elevator = transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.parent = _elevator;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
