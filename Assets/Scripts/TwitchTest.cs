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

    private TcpClient twitchClient;
    private StreamReader reader;
    private StreamWriter writer;

    public string username, password, channelName; //Get the password from https://twitchapps.com/tmi

    public TMPro.TMP_Text chatBox, errorBox;
    public Rigidbody player;
    public int speed;
    public string broadcaster_id;

    void Start()
    {
        Connect();
    }

    void Update()
    {
        if (!twitchClient.Connected)
        {
            Connect();
        }
        else
        {
            chatBox.SetText("Connected!");
        }

        //ReadChat();

        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(FillAndSend());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            reader = new StreamReader(twitchClient.GetStream());

            chatBox.text = reader.ReadToEnd();

            errorBox.SetText("none");
        }
    }

    public IEnumerator FillAndSend()
    {
        WWWForm form = new WWWForm();

        form.AddField("AppName", "Testttt");
        form.AddField("AppUser", "Reinnn");



        //UnityWebRequest www = UnityWebRequest.Post("https://api.twitch.tv/helix/channels", form);
        //www.SetRequestHeader("broadcaster_id", "codicepazzo");
        UnityWebRequest www = UnityWebRequest.Get("https://api.twitch.tv/helix/channels?broadcaster_id=141981764");

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
        //chatBox.SetText(form.data.ToString());
    }

    private void Connect()
    {
        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);

        reader = new StreamReader(twitchClient.GetStream());
        writer = new StreamWriter(twitchClient.GetStream());

        writer.WriteLine("PASS " + password);
        writer.WriteLine("NICK " + username);
        writer.WriteLine("USER " + username + " 8 * :" + username);
        writer.WriteLine("JOIN #" + channelName);

        writer.Flush();
    }

    private void ReadChat()
    {
        if (twitchClient.Available > 0)
        {
            var message = reader.ReadLine(); //Read in the current message

            chatBox.SetText(message);

            if (message.Contains("PRIVMSG"))
            {
                //Get the users name by splitting it from the string
                var splitPoint = message.IndexOf("!", 1);
                var chatName = message.Substring(0, splitPoint);
                chatName = chatName.Substring(1);

                //Get the users message by splitting it from the string
                splitPoint = message.IndexOf(":", 1);
                message = message.Substring(splitPoint + 1);
                //print(String.Format("{0}: {1}", chatName, message));
                chatBox.text = chatBox.text + "\n" + String.Format("{0}: {1}", chatName, message);

                //Run the instructions to control the game!
                GameInputs(message);
            }
        }
    }

    private void GameInputs(string ChatInputs)
    {
        if (ChatInputs.ToLower() == "left")
        {
            player.AddForce(Vector3.left * (speed * 10));
        }

        if (ChatInputs.ToLower() == "right")
        {
            player.AddForce(Vector3.right * (speed * 10));
        }

        if (ChatInputs.ToLower() == "forward")
        {
            player.AddForce(Vector3.forward * (speed * 10));
        }
    }
}