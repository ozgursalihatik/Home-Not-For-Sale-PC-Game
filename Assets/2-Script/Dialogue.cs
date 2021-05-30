using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public static Dialogue Instance;

    public TMP_Text textComponent;
    public string[ ] lines;
    public float textDuration;
    public GameObject textBox;
    public Button NextDialogueButton;

    private int index;
    private int tempOfText;
    void Start ( )
    {
        Instance = this;
        textBox.SetActive(false);
        textComponent.text = String.Empty;

    }
    private void OnMouseDown ( )
    {
        
    }
    public void StartDialogue ( )
    {
        textBox.SetActive(true);
        StartCoroutine(TypeLine( ));
    }
    IEnumerator TypeLine ( )
    {
        for ( tempOfText = 0; tempOfText < lines[index].Length; tempOfText++ )
        {
            char c = lines[index][tempOfText];
            textComponent.text += c;
            yield return new WaitForSeconds(textDuration);
            if ( tempOfText < lines[index].Length )
            {
                tempOfText++;
                StartCoroutine(TypeLine( ));
                break;
            }
        }
    }

    private void NextLine ( )
    {
        if ( index < lines.Length - 1 )
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
