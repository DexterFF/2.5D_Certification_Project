using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachTopLadder : MonoBehaviour
{
    [SerializeField]
    private Transform _bestPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player p = other.GetComponent<Player>();
            if (p != null)
            {
                p.OnTopLadderClimb(_bestPos.position);
            }
        }
    }
}
