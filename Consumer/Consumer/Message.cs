using System;

namespace Consumer
{
    public class Message<TKey, TValue>
    {
        public TKey Key { get; set; }

        public TValue Value { get; set; }

        public DateTime Timestamp { get; set; }

        public string Topic { get; set; }

        public override string ToString() => $"Key = {Key}, Value = {Value}, Timestamp = {Timestamp:f}, Topic = {Topic}";
    }
}