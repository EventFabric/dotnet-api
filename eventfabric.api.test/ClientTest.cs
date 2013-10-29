using System;
using eventfabric.api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eventfabric.api.test
{
    [TestClass]
	public class ClientTest
	{
        [TestMethod]
		public void TestSendEvents ()
		{
            var client = new Client("http", "event-fabric.com", 80, "/api/session", "/api/event", "javierdallamore", "apache");
			for (int i = 0; i < 10; i++) {
				var value = new Value (i, string.Format ("Text is {0}", i));
				var @event = new Event ("your.channel", value);
				Assert.AreEqual (System.Net.HttpStatusCode.Created, client.SendEvent (@event));   
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