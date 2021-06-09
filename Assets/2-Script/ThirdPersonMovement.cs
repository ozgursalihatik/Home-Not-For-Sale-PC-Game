using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Unity.Mathematics;

using UnityEngine;
using UnityEngine.Rendering;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;

    private Animator anim;

    public bool canJump;
    private void Start ( )
    {
        anim = GetComponent<Animator>( );
        controller = GetComponent<CharacterController>( );

        canJump = true;
    }


    // Update is called once per frame
    private void Update ( )
    {
        if ( !EventManager.GameIsLive )
            return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if ( direction.magnitude >= 0.1f )
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);

            anim.SetBool("isRun", true);

            controller.SimpleMove(moveDir);
        }
        else
        {
            anim.SetBool("isRun", false);
        }

        if ( Input.GetButtonDown("Jump") && controller.isGrounded && canJump == true )
        {
            moveDirection.y = jumpSpeed;
            anim.SetBool("isAir", true);
            StartCoroutine(WaitandChangeAnimation( ));
            StartCoroutine(WaitandTrue( ));
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

    }

    IEnumerator WaitandChangeAnimation ( )
    {
        yield return new WaitForEndOfFrame( );
        anim.SetBool("isAir", false);
        canJump = false;
    }

    IEnumerator WaitandTrue ( )
    {
        yield return new WaitForSeconds(2);
        canJump = true;
    }
}
