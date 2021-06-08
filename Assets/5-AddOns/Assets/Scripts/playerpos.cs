using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[ExecuteAlways]
public class playerpos : MonoBehaviour
{
    void Update ( )
    {
        Shader.SetGlobalVector("_PlayerPos", transform.position);
    }
}
