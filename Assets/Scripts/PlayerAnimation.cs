using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    public void CanJump(bool can)
    {
        _anim.SetBool("cnaJump",can);
    }

    public void Run(float speed)
    {
        _anim.SetFloat("Speed", Mathf.Abs(speed));
    }

    public void GrabAnimation()
    {
        _anim.SetTrigger("Grab");
        Run(0.0f);
        CanJump(false);
    }

    public void ClimbAnim()
    {
        _anim.SetTrigger("ClimbUp");
    }
}
