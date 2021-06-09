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
    public float textDuration;
    public GameObject textBox;
    public Button NextDialogueButton;
    public DialogueMessages OzgursMessages;

    private string tempMessage;
    private int index;
    private int tempOfText;
    private float timer;
    private bool isWritin;
    void Start ( )
    {
        Instance = this;
        textBox.SetActive(false);
        dialogueText.text = string.Empty;
        Application.targetFrameRate = 60;
    }
    private void Update ( )
    {
        if ( isWritin )
        {
            timer -= Time.deltaTime;
            if ( timer <= 0 )
            {
                timer += textDuration;
                tempOfText++;
                dialogueText.text = tempMessage.Substring(0, tempOfText);
                if ( tempOfText >= tempMessage.Length - 1 )
                {
                    isWritin = false;
                    return;
                }
            }
        }
    }
    public static void StartDialogueStatic ( string Message )
    {
        Instance.StartDialogue(Message);
    }
    public void ContinueDialogue()
    {
        
        isWritin = true;
    }
    private void StartDialogue ( string message )
    {
        textBox.SetActive(true);
        tempMessage = message;
        isWritin = true;
    }
}