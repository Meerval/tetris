namespace Systems.Parsers.Object
{
    public class ObjectToString : ObjectCaster<ObjectToString, string>
    {
        protected override bool Parse(object valueToParse, out string result)
        {
            if (valueToParse != null)
            {
                result = valueToParse.ToString();
                return true;
            }
            result = string.Empty;
            return false;
        }
    }
}