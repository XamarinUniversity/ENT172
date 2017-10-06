using System;

namespace ComicBook
{
	public static class ServerInfo
	{
		public static Uri AuthorizationEndpoint = new Uri("http://ent172-auth.azurewebsites.net/oauth/authorize");
		public static Uri TokenEndpoint         = new Uri("http://ent172-auth.azurewebsites.net/oauth/token");
		public static Uri ApiEndpoint           = new Uri("http://ent172-auth.azurewebsites.net/api/whoami");
		public static Uri RedirectionEndpoint 		= new Uri("http://localhost/");
		public static string ClientId 				= "A8375B66";
		public static string ClientSecret 			= "A32D8C3CBE9A";
	}
}