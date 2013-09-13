using System;
using eventfabric.api;

namespace eventfabric.api
{
    public class Event
    {
        public string channel { get; set; }
        public object value { get; set; }

        public Event(string channel, object value)
        {
            this.channel = channel;
            this.value = value;
        }
    }
}