using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    Member tmp_Member;
    int messageIndex;

    private void Awake( )
    {
        Instance = this;
    }
    void Start( )
    {
        DialogueBox.SetActive(false);
        dialogueText.text = string.Empty;
        Application.targetFrameRate = 60;
    }
    private void Update( )
    {
        if ( isWritin )
        {
            timer -= Time.deltaTime;
            if ( timer <= 0 )
            {
                if ( tempOfText >= tempMessage.Length )
                {
                    isWritin = false;
                    PrefsManager.AutoSave( );
                    return;
                }
                timer += textDuration;
                tempOfText++;
                dialogueText.text = tempMessage.Substring(0, tempOfText);
                nameText.text = tempMember;
            }
        }
    }
    public static void StartDialogueStatic( int message, Member member )
    {
        Instance.StartDialogue(message, member);
        Instance.tmp_Member = member;
        Instance.messageIndex = message;
    }
    public static DialogueMessages GetCurrentMessage( )
    {
        return Instance.Dialogues[EventManager.SessionNumber];
    }
    public void ContinueDialogue( )
    {
        StartDialogue(messageIndex, tmp_Member);
    }
    private void StartDialogue( int messages, Member member )
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
            if ( messages == 6 )
            {
                SceneManager.LoadScene(2, LoadSceneMode.Single);
            }
        }
    }
}