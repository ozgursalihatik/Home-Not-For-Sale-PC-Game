using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public static Dialogue Instance;

    public TMP_Text dialogueText, nameText;
    public float textDuration;
    public GameObject DialogueBox;
    public List<DialogueMessages> Dialogues;

    private string tempMessage, tempMember;
    private int tempOfText;
    private float timer;
    private bool isWritin;
    private void Awake ( )
    {
        Instance = this;
    }
    void Start ( )
    {
        DialogueBox.SetActive(false);
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
                nameText.text = tempMember;
                if ( tempOfText >= tempMessage.Length )
                {
                    isWritin = false;
                    PrefsManager.AutoSave( );
                    return;
                }
            }
        }
    }
    public static void StartDialogueStatic ( int message )
    {
        Instance.StartDialogue(message);
    }
    public void ContinueDialogue ( )
    {
        StartDialogue(EventManager.SessionNumber);
    }
    private void StartDialogue ( int messages )
    {
        if ( Dialogues[messages].Index < Dialogues[messages].Messages.Count )
        {
            DialogueBox.SetActive(true);
            tempMessage = Dialogues[messages].Messages[Dialogues[messages].Index];
            tempMember = Dialogues[messages].owners[Dialogues[messages].Index].ToString( );
            Dialogues[messages].Index++;
            tempOfText = 0;
            timer = 0;
            isWritin = true;
        }
        else
        {
            DialogueBox.SetActive(false);
            tempMessage = string.Empty;
            dialogueText.text = string.Empty;
            PrefsManager.AutoSave( );
            PlayerMovement.SetMovelable(true);
            EventManager.SessionNumber++;
            return;
        }
    }
}