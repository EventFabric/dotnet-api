using System;
using System.IO;
using System.Net;

namespace eventfabric.api
{
	public class Client
	{
		private string SessionPath { get; set; }

		private string EventPath { get; set; }

		private string Scheme { get; set; }

		private string Host { get; set; }

		private int Port { get; set; }

		private Cookie Cookie { get; set; }

		public Client (
            String scheme,
            String host,
            int port,
            String sessionPath,
            String eventPath,
            String userName,
            String password)
		{
			Scheme = scheme;
			Host = host;
			Port = port;
			SessionPath = sessionPath;
			EventPath = eventPath;
			UserName = userName;
			Password = password;
		}

		private HttpWebRequest CreateWebRequest (string path, Cookie cookie)
		{
			var req = WebRequest.Create ((new UriBuilder (Scheme, Host, Port, path)).Uri) as HttpWebRequest;
			if (req != null) {
				req.Method = "POST";
				req.ContentType = "application/json";
				req.Accept = "application/json";
				req.CookieContainer = new CookieContainer ();
				if (cookie != null) {
					req.CookieContainer.Add (cookie);
				}
               
				return req;
			}
			return null;
		}
        
		public HttpStatusCode SendEvent (Event @event)
		{
			try {
				var req = CreateWebRequest (EventPath, Login ());
				var eventJson = Utils.SerializeFromTToJson (@event);
				return QueryWebRequest (req, eventJson);
			} catch (Exception ex) {
				return HttpStatusCode.ServiceUnavailable;
			}
		}

		public object CreateUser (Session user)
		{
			try {
				var req = CreateWebRequest (EventPath, Login ());
				var eventJson = Utils.SerializeFromTToJson (user);
				return QueryWebRequest (req, eventJson);
			} catch (Exception ex) {
				return HttpStatusCode.ServiceUnavailable;
			}
		}

		private Cookie Login ()
		{
			if (Cookie != null && Cookie.Expires >= DateTime.Now) {
				return Cookie;
			}

			var req = CreateWebRequest (SessionPath, Cookie);
			var json = Utils.SerializeFromTToJson (new Session (UserName, Password, ""));
			QueryWebRequest (req, json);
			return Cookie;
		}

		protected string UserName { get; set; }
        
		protected string Password { get; set; }

		private HttpStatusCode QueryWebRequest (HttpWebRequest req, string json)
		{
			if (req != null) {
				var writer = new StreamWriter (req.GetRequestStream ());
				writer.Write (json);
				writer.Close ();

				using (var resp = req.GetResponse() as HttpWebResponse) {
					if (resp != null && resp.Cookies.Count > 0) {
						Cookie = resp.Cookies [0];
						Cookie.Path = "/";
					}
					return resp.StatusCode;
				}
			}
			return HttpStatusCode.ServiceUnavailable;
		}
	}
}
