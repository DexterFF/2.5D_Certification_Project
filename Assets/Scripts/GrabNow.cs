using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabNow : MonoBehaviour
{
    [SerializeField]
    private Vector3 _perfectGrapPos, _perfectStandPos;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("LedgeToGrab"))
        {
            PlayerAnimation pAnimation = other.transform.parent.GetComponent<PlayerAnimation>();
            Player p = other.transform.parent.GetComponent<Player>();

            if(p != null && pAnimation != null)
            {
                p.Grabing(true);
                p.GrabScript(this);
                p.SnapGrapPosition(_perfectGrapPos);
                pAnimation.GrabAnimation();
            }
        }
    }

    public Vector3 StandPosition()
    {
        return _perfectStandPos;
    }
}
