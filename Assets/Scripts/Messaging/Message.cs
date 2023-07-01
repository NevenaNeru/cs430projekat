using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum messageType
{
    startFollow, stopFollow, callSlime, stopSlime
}

public class Message
{
    public int sender;
    public int reciver;
    public messageType message;
    public float dispatchTime;

    public Message(int s, int re, messageType m, float dp)
    {
        sender = s;
        reciver = re;
        message = m;
        dispatchTime = dp;
    }
}
