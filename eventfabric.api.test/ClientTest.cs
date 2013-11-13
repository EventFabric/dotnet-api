using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eventfabric.api.test
{
    [TestClass]
	public class ClientTest
	{
        [TestMethod]
		public void TestSendEvents ()
		{
            var client = new Client("http", "event-fabric.com", 80, "/api/session", "/api/event");
            var loginResponse = client.Login("demouser", "demouser");
            Assert.AreEqual(System.Net.HttpStatusCode.Created, Enum.Parse(typeof(System.Net.HttpStatusCode), loginResponse.StatusCode.ToString()));

			for (int i = 0; i < 3; i++) {
				var value = new Value (i, string.Format ("Text is {0}", i));
				var @event = new Event ("your.channel", value);
                var response = client.SendEvent(@event, loginResponse.Cookies);
                Assert.AreEqual(System.Net.HttpStatusCode.Created, Enum.Parse(typeof(System.Net.HttpStatusCode), response.StatusCode.ToString()));
			}
		}
	}

	public class Value
	{
		public Value (int value, String text)
		{
			this.value = value;
			this.text = text;
		}

		public int value { get; set; }

		public String text { get; set; }
	}
}