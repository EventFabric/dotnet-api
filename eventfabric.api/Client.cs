using System;
using System.IO;
using System.Net;
using System.Text;

namespace eventfabric.api
{
	public class Client
	{
		private string SessionPath { get; set; }

		private string EventPath { get; set; }

		private string Scheme { get; set; }

		private string Host { get; set; }

		private int Port { get; set; }

		public Client (
            String scheme,
            String host,
            int port,
            String sessionPath,
            String eventPath)
		{
			Scheme = scheme;
			Host = host;
			Port = port;
			SessionPath = sessionPath;
			EventPath = eventPath;
		}

        public Client()
            : this("http", "event-fabric.com", 80, "/api/session", "/api/event")
        {
            
        }

		private HttpWebRequest CreateWebRequest (string path, CookieCollection cookies)
		{
			var req = WebRequest.Create ((new UriBuilder (Scheme, Host, Port, path)).Uri) as HttpWebRequest;
			if (req != null) {
				req.Method = "POST";
				req.ContentType = "application/json";
				req.Accept = "application/json";
				req.CookieContainer = new CookieContainer();
				if (cookies != null) {
				    foreach (var cookie in cookies)
				    {
                        req.CookieContainer.Add((Cookie)cookie);    
				    }
					
				}
               
				return req;
			}
			return null;
		}
        
		public Response SendEvent (Event @event, CookieCollection cookie)
		{
            var req = CreateWebRequest(EventPath, cookie);
			var eventJson = Utils.SerializeFromTToJson(@event);
			return QueryWebRequest(req, eventJson);
		}

        public Response Login(string username, string password)
		{
			var req = CreateWebRequest (SessionPath, null);
			var json = Utils.SerializeFromTToJson (new Session (username, password));
			return QueryWebRequest (req, json);			
		}

        private Response QueryWebRequest(HttpWebRequest req, string json)
		{
			if (req != null) {
				var writer = new StreamWriter (req.GetRequestStream ());
				writer.Write (json);
				writer.Close ();

				using (var resp = req.GetResponse() as HttpWebResponse) {
				    if (resp != null)
				    {
				        var body = GetBody(resp);
                        return new Response(resp.StatusDescription, Convert.ToInt32(resp.StatusCode), body, resp.Cookies);
				    }
				}
			}
            return new Response((string) "ServiceUnavailable", Convert.ToInt32(HttpStatusCode.ServiceUnavailable));
		}

	    private String GetBody(HttpWebResponse resp)
	    {
	        var body = new StringBuilder();
            // Gets the stream associated with the response.
            var receiveStream = resp.GetResponseStream();
            var encode = System.Text.Encoding.GetEncoding("utf-8");
            // Pipes the stream to a higher level stream reader with the required encoding format. 
	        if (receiveStream != null)
	        {
	            var readStream = new StreamReader(receiveStream, encode);
	            var read = new Char[256];
	            // Reads 256 characters at a time.     
	            var count = readStream.Read(read, 0, 256);
	            while (count > 0)
	            {
	                // Dumps the 256 characters on a string and displays the string to the console.
	                var str = new String(read, 0, count);
	                body.Append(str);
	                count = readStream.Read(read, 0, 256);
	            }
	        }
	        return body.ToString();
	    }
	}
}
