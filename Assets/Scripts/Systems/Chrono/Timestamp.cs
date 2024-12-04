using System;

namespace Systems.Chrono
{
    public class Timestamp : ITimestamp
    {
        private const string DefaultFormat = "yyyy-MM-dd'T'HH:mm:ss.fffzzz";
        
        public static Timestamp Now => new (DateTime.Now);
        public static Timestamp Empty => new (DateTime.MinValue);

        private DateTime Time { get; set; }
        
        private Timestamp(DateTime dateTime)
        {
            Time = dateTime;
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