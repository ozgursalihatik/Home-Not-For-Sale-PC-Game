using System.Collections.Generic;

using UnityEngine;

public class Grandpa : MonoBehaviour
{
    public int MessageNumber = 0;
    public DialogueMessages Message;
    public string npcName;

    public Grandpa ( int messageNumber, DialogueMessages message, string Name )
    {
        MessageNumber = messageNumber;
        Message = message;
        npcName = Name;
    }
}