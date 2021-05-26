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
    public float textSpeed;
    public GameObject textBox;

    private int index;
    private int tempOfText;
    void Start ( )
    {
        instance = this;
        textBox.SetActive(false);
        textComponent.text = String.Empty;

    }

    // Update is called once per frame
    void Update ( )
    {

    }

    public void StartDialogue ( )
    {

        textBox.SetActive(true);
        //index = 0;
        StartCoroutine(TypeLine( ));

    }

    IEnumerator TypeLine ( )
    {
        for ( int i = 0; tempOfText < lines[index].Length; tempOfText++ )
        {
            char c = lines[index][tempOfText];
            textComponent.text += c;
            yield return new WaitForSeconds(.03f);
            if ( tempOfText < lines[index].Length )
            {
                tempOfText++;
                StartCoroutine(TypeLine( ));
                break;
            }
            //yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine ( )
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
