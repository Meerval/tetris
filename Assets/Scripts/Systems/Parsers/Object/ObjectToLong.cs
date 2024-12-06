namespace Systems.Parsers.Object
{
    public class ObjectToLong : ObjectCaster<ObjectToLong, long>
    {
        protected override bool Parse(object valueToParse, out long result)
        {
            if (long.TryParse(valueToParse.ToString(), out result))
            {
                return true;
            }
            result = -1L;
            return false;
        }
    }
}