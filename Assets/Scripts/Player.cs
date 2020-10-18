using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _pController;
    private PlayerAnimation _playerAnim;

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

    // Start is called before the first frame update
    void Start()
    {
        _pController = GetComponent<CharacterController>();
        _playerAnim = GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGrabing == false)
            Mouvements();
        else
            CanClimb();
    }

    public void CanClimb()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            _playerAnim.ClimbAnim();
        }
    }

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
        transform.position = pos;
    }

    public void StandUpAfterClimb()
    {
        transform.position = _grapScript.StandPosition();
    }
}
