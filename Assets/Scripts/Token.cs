using UnityEngine;

[System.Serializable]
public struct Token
{
    [SerializeField] public string access_token;
    [SerializeField] public string expires_in;
    [SerializeField] public string token_type;
}