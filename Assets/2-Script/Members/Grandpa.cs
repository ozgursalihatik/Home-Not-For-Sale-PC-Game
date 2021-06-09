﻿using System.Collections.Generic;

using UnityEngine;

public class Grandpa : MonoBehaviour
{
    public int MessageNumber = 0;
    public DialogueMessages Message;

    public Grandpa ( int messageNumber, DialogueMessages message )
    {
        MessageNumber = messageNumber;
        Message = message;
    }
}