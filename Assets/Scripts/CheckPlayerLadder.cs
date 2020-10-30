using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerLadder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Player p = other.GetComponent<Player>();
            if(p != null)
            {
                p.CanClimbLadder(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player p = other.GetComponent<Player>();
            if (p != null)
            {
                p.CanClimbLadder(false);
            }
        }
    }
}
