using System;
using Templates.POCO;

namespace Systems.Chrono
{
    public class Timestamp : ITimestamp
    {
        private const string DefaultFormat = "yyyy-MM-dd'T'HH:mm:ss.fffzzz";

        private DateTime Time { get; set; }

        public Timestamp()
        {
            Time = DateTime.Now;
        }

        public Timestamp(Timestamp timestamp)
        {
            Time = timestamp.Time;
        }

        public Timestamp(string timestamp)
        {
            Time = DateTime.Parse(timestamp);
        }

        public override string ToString()
        {
            return Time.ToString(DefaultFormat);
        }
    }
}