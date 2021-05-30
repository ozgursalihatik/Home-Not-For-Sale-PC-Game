using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public static Dialogue instance;
    public TextMeshProUGUI textComponent;
    public string[ ] lines;
    public float textDuration;
    public GameObject textBox;

    private int index;
    private int tempOfText;
    void Start ( )
    {
        instance = this;
        textBox.SetActive(false);
        textComponent.text = String.Empty;

    }
    void Update ( )
    {
        if ( Input.GetMouseButtonDown(0) )
        {

        }
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
    private void OnMouseDown ( )
    {
        
    }
}
