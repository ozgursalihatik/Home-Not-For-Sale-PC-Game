using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    public CharacterController _controller;

    public static Vector3 Position { get => Instance.transform.position; set => Instance.transform.position = value; }
    public static Quaternion Rotation { get => Instance.transform.rotation; set => Instance.transform.rotation = value; }

    public float _speed = 12f;
    public float _gravity = -9.81f;
    public float _jumpheight = 10f;

    private Vector3 velocity;
    private int SessionNumber { get => EventManager.SessionNumber; }

    public Animator _anim;

    [SerializeField] bool _isGround;
    [SerializeField] private GameObject _player;

    [SerializeField] public bool canMove;

    private bool temp = true;
    private void OnEnable( )
    {
        PrefsManager.OnAwake += Awake;
    }

    private void Awake( )
    {
        Instance = this;
    }
    void Start( )
    {
        _anim = GetComponent<Animator>( );
        _isGround = true;
        canMove = true;
    }

    public void Update( )
    {
        if ( !canMove )
            return;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        _controller.Move(move * _speed * Time.deltaTime);

        if ( move.magnitude > 0.1f )
        {
            _anim.SetBool("isRun", true);
        }
        else
        {
            _anim.SetBool("isRun", false);
        }

        velocity.y += _gravity * Time.deltaTime;

        _controller.Move(velocity * Time.deltaTime);

        if ( Input.GetButtonDown("Jump") && _isGround == true )
        {
            _anim.SetBool("isJump", true);
            velocity.y = Mathf.Sqrt(_jumpheight * -2f * _gravity);
            _isGround = false;
            StartCoroutine(waitandjump( ));
        }
    }
    IEnumerator waitandjump( )
    {
        yield return new WaitForSeconds(1.5f);
        _isGround = true;
        _anim.SetBool("isJump", false);
    }
    private void OnTriggerEnter( Collider other )
    {
        if ( other.gameObject.tag == "Touch" )
        {
            _anim.SetBool("inTouch", true);
        }
        Member member;

        if ( other.gameObject.TryGetComponent(out member) && temp )
        {
            if ( member.MemberIndex == EventManager.SessionNumber )
            {
                DialogueMessages scriptable = Dialogue.GetCurrentMessage( );
                if ( scriptable.Index >= scriptable.Messages.Count )
                {
                    StartCoroutine(tempDelay( ));
                    return;
                }
                else
                {
                    PrefsManager.AutoSave( );
                    Dialogue.StartDialogueStatic(member.MemberIndex, member);
                    _anim.SetBool("isRun", false);
                    _anim.SetBool("isJump", false);
                    canMove = false;
                    EventManager.GameIsLive = false;
                    Cursor.lockState = CursorLockMode.Confined;
                    temp = false;
                    StartCoroutine(tempDelay( ));
                    return;
                }
            }
        }
        else if ( other.CompareTag("Wood") )
        {

        }
        else if ( other.CompareTag("Plank") )
        {

        }
        else if ( other.CompareTag("Eggs") )
        {

        }
    }
    public static void SetMovelable( bool state )
    {
        Instance.canMove = state;
        EventManager.GameIsLive = state;
        if ( state )
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
    public IEnumerator tempDelay( )
    {
        yield return new WaitForSeconds(5f);
        temp = true;
    }
}
