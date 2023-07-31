using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TwitchOauthLogin : MonoBehaviour
{
    static public Action<Token> OnGetToken;
    public Token MyToken;
    [SerializeField] TMPro.TMP_Text tokenText, errorText;
    // Start is called before the first frame update
    void Start()
    {
        OnGetToken += (Token token) => { tokenText.SetText(token.access_token); MyToken = token; };

        StartCoroutine(GetAccessToken(OnGetToken));
    }

    private static IEnumerator GetAccessToken(Action<Token> result)
    {
        WWWForm content = new WWWForm();

        content.AddField("grant_type", "client_credentials"); //authorization_code
        content.AddField("client_id", Secrets.ClientID);
        content.AddField("client_secret", Secrets.ClientSecret);

        UnityWebRequest www = UnityWebRequest.Post(Secrets.AccessTokenURL, content);

        //Send request
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.ConnectionError)
        {
            string resultContent = www.downloadHandler.text;
            Secrets.ACCESS_TOKEN = JsonUtility.FromJson<Token>(resultContent);

            //Return result
            result(Secrets.ACCESS_TOKEN);
        }
        else
        {
            //Return null
            Debug.Log("errore");
            //result("");
        }
    }
}
