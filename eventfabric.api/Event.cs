using System;
using eventfabric.api;

namespace eventfabric.api
{
    public class Event
    {
        public string channel { get; set; }
        public object value { get; set; }

        public Event()
        {
            
        }

        public Event(string channel, object value)
        {
            this.channel = channel;
            this.value = value;
        }

        public override String ToString()
        {
            return String.Format("Channel: {0}\\nValue: {1}", channel, value);
        }
    }
}