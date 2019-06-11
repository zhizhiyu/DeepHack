using UnityEngine;
using System.Collections;
using System.Collections.Generic;



/****************************************************************
 *Copyright(C)  2019 by luojinming All rights reserved. 
 *FileName:     messageCenter.cs
 *Author:       luojinming 
 *Version:      1.0 
 *Date:         6.11
 *Description:  消息中心，用于传递消息。发送消息可以使用sendMessage,
 *              在需要发送消息的game object的脚本代码上面使用此函数。
 *              也可以使用addListener函数来监听该消息事件，在需要接受此消息的game object的脚本上使用此函数。
 *              
 *History:     1: 6.11,15:12 
*****************************************************************/

public class MessageCenter
{
    //委托函数
    public delegate void DelegateMessage(KeyValue keyValue);
    
    //保存消息的容器
    public static Dictionary<string, DelegateMessage> messages = new Dictionary<string, DelegateMessage>();


    //在需要接受消息的game object的脚本上使用此函数，来增加监听事件
    //messageType：字符串形式的消息类型。例如"collision"
    //delegateMessage：处理该消息的函数。该函数的格式为：void process（KeyValues kv)。
    public static void AddListener(string messageType, DelegateMessage delegateMessage)
    {
        if(messages.ContainsKey(messageType))
        {
            Debug.Log("message center contains the message type: " + messageType);
            messages[messageType] += delegateMessage;
        }
        else
        {
            Debug.Log("message center does not contain the message type: " + messageType + ", and now add the message type");
            messages.Add(messageType, null);
            messages[messageType] += delegateMessage;
        }

    }

    //删除监听事件
    //messageType：字符串形式的消息类型。例如"collision"
    //delegateMessage：处理该消息的函数。该函数的格式为：void process（KeyValues kv)。
    public static void DeleteListener(string messageType, DelegateMessage delegateMessage)
    {
        if(messages.ContainsKey(messageType))
        {
            messages[messageType] -= delegateMessage;
        }
        else
        {
            Debug.LogError("ERROR: message center does not contain the message type: " + messageType);
        }
    }

    //sender发送消息给所有接收者
    //messageType：  字符串形式的消息类型。例如"collision"
    //keyValue：     消息的内容，键值对形式
    public static void SendMessage(string messageType, KeyValue keyValue)
    {
        DelegateMessage delegateMessage;
        if(messages.TryGetValue(messageType, out delegateMessage))
        {
            if(delegateMessage != null)
            {
                delegateMessage(keyValue);
            }
            else
            {
                Debug.Log("the message type: " + messageType + " does not have any listener!");
            }
        }
        else
        {
            //Debug.Log("message center does not contain the message type: " + messageType + ", and now add the message type");
            //messages.Add(messageType, null);
            //Debug.LogError("ERROR: message center does not contain the message type: " + messageType);
        }
    }

    //发送消息的函数的进一步封装，也是用于发送消息
    //messageType：  字符串形式的消息类型。例如"collision"
    //messageKey:    消息的key，说明消息的名称
    //messageVlalue：消息的value， 说明了消息的值，可以为任何基本类型
    public static void SendMessage(string messageType, string messageKey, object messageValue)
    {
        KeyValue keyValue = new KeyValue(messageKey, messageValue);
        SendMessage(messageType, keyValue);
    }


}
