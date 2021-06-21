using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class FirstSceneConroller : MonoBehaviour
{
    private void Update( )
    {
        if ( !GetComponent<VideoPlayer>( ).isPlaying )
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
            return;
        }
    }

}
