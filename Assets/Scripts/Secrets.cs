public static class Secrets
{
	public const string CLIENT_ID = "s1tqaya75jdt1ysnpi6cwpfu8wmone"; //Your application's client ID, register one at https://dev.twitch.tv/dashboard
	public const string CLIENT_SECRET = "ygermjrl3scy11vvvf93nm1ezfdqnv"; //Your application's client ID, register one at https://dev.twitch.tv/dashboard
	public static Token ACCESS_TOKEN; //A Twitch OAuth token which can be used to connect to the chat
	public const string REFRESH_TOKEN= "0tpl3f970nl5jc2vx51h36y6uaysaxqu2ohgnjvm27v3o3pkji"; //The username which was used to generate the OAuth token
	public const string USERNAME_FROM_OAUTH_TOKEN = "ximergod";
	public const string CHANNEL_ID_FROM_OAUTH_TOKEN = "93832921"; //The channel Id from the account which was used to generate the OAuth token

	public const string AuthURL = "https://id.twitch.tv/oauth2/authorize";
	public const string AccessTokenURL = "https://id.twitch.tv/oauth2/token";
	public const string TestURL = "https://api.twitch.tv/helix/channels/followers?broadcaster_id=93832921";
	public const string CallBackURL = "http://localhost";
	public const string ClientID = "s1tqaya75jdt1ysnpi6cwpfu8wmone";
	public const string ClientSecret = "loo3449d9y6xys540w9ff49vfdbxiq";
	public const string Scope = "chat:read channel:read:redemptions channel:manage:redemptions";
}
