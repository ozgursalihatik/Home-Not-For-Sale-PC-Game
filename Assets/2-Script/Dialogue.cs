using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public static Dialogue Instance;

    public TMP_Text dialogueText;
    public List<string> lines;
    public float textDuration;
    public GameObject textBox;
    public Button NextDialogueButton;

    private int index;
    private int tempOfText;
    void Start ( )
    {
        Instance = this;
        textBox.SetActive(false);
        dialogueText.text = string.Empty;

    }
    private void OnMouseDown ( )
    {

    }
    public void StartDialogue ( object Owner )
    {
        if ( Owner.Equals(typeof(Grandpa)) )
        {
            textBox.SetActive(true);
            StartCoroutine(TypeLine( ));
        }
    }
    IEnumerator TypeLine ( )
    {
        for ( tempOfText = 0; tempOfText < lines[index].Length; tempOfText++ )
        {
            char c = lines[index][tempOfText];
            dialogueText.text += c;
            yield return new WaitForSeconds(textDuration);
            if ( tempOfText < lines[index].Length )
            {
                tempOfText++;
                StartCoroutine(TypeLine( ));
                break;
            }
        }
    }

    public void Continue ( )
    {

    }

    private void NextLine ( )
    {
        if ( index < lines.Count - 1 )
        {
            //index++;
            //textComponent.text = string.Empty;
            StartCoroutine(TypeLine( ));
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}