using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Member : MonoBehaviour
{
    public int MemberIndex;

    public bool isWalking, isTalking1, isTalking2;
    public float walkSpeed;

    private Vector3 _target;
    public Vector4 Target
    {
        set
        {
            if ( value.w > 0 )
            {
                _target = new Vector3(value.x, value.y, value.z);
                walkSpeed = value.w;
            }
        }
    }

}