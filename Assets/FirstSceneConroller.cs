using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class FirstSceneConroller : MonoBehaviour
{
    private bool isPlaying;
    private void Start( )
    {
        StartCoroutine(firstDelay( ));
    }
    private IEnumerator firstDelay( )
    {
        yield return new WaitForSecondsRealtime((float)GetComponent<VideoPlayer>( ).clip.length);
        SceneManager.LoadScene(1);
    }
}
