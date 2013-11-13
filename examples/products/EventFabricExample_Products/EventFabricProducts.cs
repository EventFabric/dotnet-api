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
        private readonly Client _client;
        private Response _loginResponse; 
        private bool _isRunning;

        public EventFabricProducts()
        {
            _client = new Client("http", "event-fabric.com", 80, "/api/session", "/api/event");
            InitializeComponent();
        }

        private void EventFabricProductsLoad(object sender, EventArgs e)
        {

        }

        private void BtnConnectClick(object sender, EventArgs e)
        {
            _loginResponse = _client.Login(txtUsername.Text, txtPassword.Text);
            var responseText = String.Format("Status: {0} {1}",
                                                _loginResponse.StatusCode, _loginResponse.StatusDescription);

            txtResponse.Text = responseText;
        }

        private void BtnPlayClick(object sender, EventArgs e)
        {
            Play();
        }

        private void Play()
        {
            _isRunning = btnPlay.Text == Resources.EventFabricProducts_Play_Play;

            btnPlay.Text = btnPlay.Text == Resources.EventFabricProducts_Play_Play
                               ? Resources.EventFabricProducts_Play_Pause
                               : Resources.EventFabricProducts_Play_Play;

            if (_isRunning)
            {
                var oThread = new Thread(RunSendEvent);
                // Start the thread
                oThread.Start();
            }

        }

        private void RunSendEvent()
        {
            if (_isRunning)
            {
                SendEvent();
                Thread.Sleep(Convert.ToInt32(txtInterval.Text) * 1000);
                RunSendEvent();
            }
        }

        private void BtnSendClick(object sender, EventArgs e)
        {
            txtResponse.Text = SendEvent();
        }

        private String SendEvent()
        {
            if (_loginResponse == null || _loginResponse.StatusCode != 201)
            {
                _loginResponse = _client.Login(txtUsername.Text, txtPassword.Text);
            }

            var products = txtProducts.Text.Split(',');
            var index = new Random().Next(0, products.Length);
            var product = products[index];
            var count = new Random().Next(100);
            var price = new Random().NextDouble();
            var delivered = new Random().Next(100) > 50;
            var value = new Product(product, count, price, delivered);

            var @event = new Event(txtChannel.Text, value);

            var response = _client.SendEvent(@event, _loginResponse.Cookies);

            var responseText = String.Format("Status: {0} {1}\n Event:\n{2}",
                                                response.StatusCode, response.StatusDescription, @event);

            return responseText;
        }
    }
}