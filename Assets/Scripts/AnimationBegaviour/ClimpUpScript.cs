using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimpUpScript : StateMachineBehaviour
{
    private Player _player;

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = animator.transform.parent.GetComponent<Player>();
        if(_player != null)
        {
            _player.StandUpAfterClimb();
            _player.Grabing(false);
        }
    }
}
