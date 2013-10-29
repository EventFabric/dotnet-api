using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using EventFabricExample_Products.Properties;
using eventfabric.api;

namespace EventFabricExample_Products
{
    public partial class EventFabricProducts : Form
    {
        private Client _client;

        public EventFabricProducts()
        {
            InitializeComponent();
        }

        private void EventFabricProducts_Load(object sender, EventArgs e)
        {

        }

        private void BtnConnectClick(object sender, EventArgs e)
        {
            Connect();
        }

        private void BtnPlayClick(object sender, EventArgs e)
        {
            Play();
        }

        private void Play()
        {
            btnPlay.Text = btnPlay.Text == Resources.EventFabricProducts_Play_Play
                               ? Resources.EventFabricProducts_Play_Pause
                               : Resources.EventFabricProducts_Play_Play;
        }

        private void BtnSendClick(object sender, EventArgs e)
        {
            SendEvent();
        }

        private void Connect()
        {
            _client = new Client("http", "event-fabric.com", 80, "/api/session", "/api/event", txtUsername.Text,
                                 txtPassword.Text);
        }

        private void SendEvent()
        {
            if (_client == null)
            {
                Connect();
            }

            var products = txtProducts.Text.Split(',');
            var index = new Random().Next(0, products.Length);
            var product = products[index];
            var count = new Random().Next(100);
            var price = new Random().NextDouble();
            var delivered = new Random().Next(100) > 50;
            var value = new Product(product, count, price, delivered);

            var @event = new Event(txtChannel.Text, value);
            HttpStatusCode response = _client.SendEvent(@event);

            String responseText = String.Format("Status: {0}\nEvent:\n{1}",
                                                response, @event);

            txtResponse.Text = responseText;

        }
    }
}