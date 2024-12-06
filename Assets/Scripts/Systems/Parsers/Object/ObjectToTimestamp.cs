using System;
using Systems.Chrono;

namespace Systems.Parsers.Object
{
    public class ObjectToTimestamp : ObjectCaster<ObjectToTimestamp, Timestamp>
    {
        protected override bool Parse(object valueToParse, out Timestamp result)
        {
            string strTimestamp = ObjectToString.TryParse(valueToParse).OrElse(string.Empty);
            try
            {
                result = new Timestamp(strTimestamp);
                return true;
            }
            catch (Exception)
            {
                result = Timestamp.Empty;
                return false;
            }
        }
    }
}