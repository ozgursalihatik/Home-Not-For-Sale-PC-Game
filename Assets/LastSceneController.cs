using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LastSceneController : MonoBehaviour
{
    public List<DialogueMessages> dialogues;
    private void Start( )
    {
        StartCoroutine(LastDelay( ));
    }
    private IEnumerator LastDelay( )
    {
        yield return new WaitForSeconds(10);
        for ( int i = 0; i < dialogues.Count; i++ )
        {
            dialogues[i].Index = 0;
        }
        PlayerPrefs.DeleteAll( );
        Application.Quit( );
    }
}
