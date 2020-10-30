using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _pController;
    private PlayerAnimation _playerAnim;

    [Header("Player Mouvement Jump Behavior")]
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _gravity;
    private Vector3 _velocity;
    private float _velocityY;
    [SerializeField]
    private float _jumpHeightIdle;
    [SerializeField]
    private float _jumpHeightRunning;
    private bool _canJump;

    private bool _isGrabing = false;
    private GrabNow _grapScript;

    [Header("Player Ladder Climb System")]
    [SerializeField]
    private float _speedClimbLadder;
    private bool _canClimbLadder = false;
    private bool _onLadder = false;
    private Vector3 _ladderTopPos;

    // Start is called before the first frame update
    void Start()
    {
        _pController = GetComponent<CharacterController>();
        _playerAnim = GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_onLadder == false)
        {
            if (_canClimbLadder == true)
                ClimbLadder();

            if (_isGrabing == false)
                Mouvements();
            else
                CanClimb();
        }
        else
        {
            MouvementOnLadder();
        }
    }

    /*------------------------------- PLAYER MOUVEMENT JUMP SYSTEM --------------------------------*/
    public void Mouvements()
    {
        if(_pController != null)
        {
            if (_pController.isGrounded == true)
            {
                float horiz = Input.GetAxisRaw("Horizontal");
                Vector3 direction = new Vector3(0, 0, horiz);
                _velocity = direction * _speed;
                _playerAnim.Run(horiz);
                Jump(horiz);
                FlipPlayer(horiz);
            }
            else
            {
                _velocityY -= _gravity;
            }

            _velocity.y = _velocityY;

            _pController.Move(_velocity * Time.deltaTime);
        }
    }

    public void Jump(float RuningOrNot)
    {
        if (_canJump == false)
        {
            _playerAnim.CanJump(_canJump);
            _canJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (RuningOrNot != 0)
            {
                _velocityY = _jumpHeightRunning;
                _playerAnim.CanJump(_canJump);
                _canJump = false;
            }
            else
            {
                _velocityY = _jumpHeightIdle;
            }
        }
    }

    public void FlipPlayer(float directionToFace)
    {
        if (directionToFace < 0)
        {
            transform.rotation = Quaternion.AngleAxis(180f, Vector3.up);
        }
        else if (directionToFace > 0)
        {
            transform.rotation = Quaternion.AngleAxis(0f, Vector3.up);
        }
    }

    /*--------------------------------------- JUMP GRAP AND CLIMB ON LEDGE SYSTEM -----------------------------------*/
    public void CanClimb()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _playerAnim.ClimbAnim();
        }
    }

    public void Grabing(bool isIt)
    {
        _isGrabing = isIt;
    }

    public void GrabScript(GrabNow grabSc)
    {
        _grapScript = grabSc;
    }

    public void SnapGrapPosition(Vector3 pos)
    {
        pos.y -= 4.59401f;
        transform.position = pos;
    }

    public void StandUpAfterClimb()
    {
        transform.position = _grapScript.StandPosition();
    }

    /*--------------------------------------- COLLECTABLE ----------------------------*/
    private void OnTriggerEnter(Collider other)
    {
        Collectable c = other.GetComponent<Collectable>();
        if(c != null)
        {
            c.Collected();
        }
    }

    /*--------------------- LADDER SYSTEM -----------------------------*/

    public void ClimbLadder()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _onLadder = true;
            _playerAnim.ClimbLadderIdle();
        }
    }

    public void MouvementOnLadder()
    {
        float vert = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(0, vert, 0);
        _velocity = direction * _speedClimbLadder;

        if (_pController.isGrounded == false)
        {
            _pController.Move(_velocity * Time.deltaTime);
            _playerAnim.ClimbSpeed(Mathf.Abs(vert));
        }
        else
        {
            if (vert > 0)
            {
                _pController.Move(_velocity * Time.deltaTime);
                _playerAnim.ClimbSpeed(Mathf.Abs(vert));
            }
            else
            {
                _playerAnim.ClimbSpeed(0);
                if (Input.GetKeyDown(KeyCode.S))
                {
                    _onLadder = false;
                    _playerAnim.ForLadderToId();
                }
            }
        }
    }

    public void OnTopLadderClimb(Vector3 pos)
    {
        if (_onLadder == true)
        {
            _playerAnim.OnTopLadder();
            _ladderTopPos = pos;
        }
    }

    public void SnapPoisitionAfterClimbLadderTop()
    {
        transform.position = _ladderTopPos;
    }

    public void OnLadder(bool b)
    {
        _onLadder = b;
    }

    public void CanClimbLadder(bool can)
    {
        _canClimbLadder = can;
    }
}
