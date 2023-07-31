using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TwitchTest : MonoBehaviour
{
    //private TcpClient twitchClient;
    //private StreamReader reader;
    //private StreamWriter writer;

    public TMPro.TMP_Text chatBox, errorBox;
    public string url;

    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    Connect();
        //}

        //ReadChat();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(SendRequest());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            chatBox.SetText("none");
            errorBox.SetText("none");
        }
    }

    public IEnumerator SendRequest()
    {
        UnityWebRequest www = UnityWebRequest.Get(url + "?" + "broadcaster_id=" + Secrets.CHANNEL_ID_FROM_OAUTH_TOKEN);
        www.SetRequestHeader("Client-ID", Secrets.ClientID);
        www.SetRequestHeader("Authorization", "Bearer lciles9dnn5p383x9oulolzwjfqj1n");

        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            errorBox.SetText(www.error);
        }
        else
        {
            errorBox.SetText(www.downloadHandler.text);
        }

        chatBox.SetText(www.result.ToString());
    }

    //private void Connect()
    //{
    //    twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);

    //    reader = new StreamReader(twitchClient.GetStream());
    //    writer = new StreamWriter(twitchClient.GetStream());

    //    writer.WriteLine("PASS " + password);
    //    writer.WriteLine("NICK " + username);
    //    writer.WriteLine("USER " + username + " 8 * :" + username);
    //    writer.WriteLine("JOIN #" + channelName);

    //    writer.Flush();
    //}

}