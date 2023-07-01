using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDispatcher
{
    private static MessageDispatcher md = null;
    private List<Message> messages = new List<Message>();

    private MessageDispatcher() { }

    public static MessageDispatcher Instance()
    {
        if (MessageDispatcher.md == null)
        {
            MessageDispatcher.md = new MessageDispatcher();
        }
        return MessageDispatcher.md;
    }

    public void Update()
    {
        MessageDispatcher.Instance()._Update();
    }

    private void _Update()
    {
        List<Message> newMessages = new List<Message>();

        for (int i = 0; i < this.messages.Count; i++)
        {
            Message Message = this.messages[i];
            if (Message.dispatchTime <= 0)
            {
                this.Discharge(Message);
            }
            else
            {
                newMessages.Add(Message);
            }

        }
        this.messages = newMessages;
    }

    public void Discharge(Message Message)
    {
        List<object> list;

        object reciver = EntityManager.getEntity(Message.reciver);
        list = new List<object>();
        list.Add(reciver);

        foreach (IReciver entity in list)
        {
            if (entity != null && entity is IReciver)
            {

                ((IReciver)entity).HandleMessage(Message);
            }
        }

    }

    public void Dispatch(float delay, int sender, int reciver, messageType mt)
    {
        Message message = new Message(sender, reciver, mt, Time.time + delay);

        if (delay <= 0)
        {
            this.Discharge(message);
        }
        else
        {
            this.messages.Add(message);
        }
    }
}
