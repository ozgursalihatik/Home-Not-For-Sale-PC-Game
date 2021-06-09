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
    private GameSessions currentSession { get => EventManager.CurrentSession; }

    public Animator _anim;

    [SerializeField] bool _isGround;
    [SerializeField] private GameObject _player;

    [SerializeField] private bool canMove;

    private void Awake ( )
    {
        Instance = this;
    }
    void Start ( )
    {
        _anim = GetComponent<Animator>( );
        _isGround = true;
        canMove = true;
    }

    public void Update ( )
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
    IEnumerator waitandjump ( )
    {
        yield return new WaitForSeconds(1f);
        _isGround = true;
        _anim.SetBool("isJump", false);
    }
    private void OnTriggerEnter ( Collider other )
    {
        if ( other.gameObject.tag == "Touch" )
        {
            _anim.SetBool("inTouch", true);
        }

        if ( other.gameObject.TryGetComponent(out Grandpa grandpa) )
        {
            PrefsManager.AutoSave( );
            Dialogue.StartDialogueStatic(grandpa.Message.Messages[grandpa.MessageNumber]);
            EventManager.CurrentSession = GameSessions.Grandpa;
            canMove = false;
            EventManager.GameIsLive = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
