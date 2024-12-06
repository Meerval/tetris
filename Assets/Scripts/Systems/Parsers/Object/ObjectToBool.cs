namespace Systems.Parsers.Object
{
    public class ObjectToBool : ObjectCaster<ObjectToBool, bool>
    {
        protected override bool Parse(object valueToParse, out bool result)
        {
            if (bool.TryParse(valueToParse.ToString(), out result))
            {
                return true;
            }
            result = false;
            return false;
        }
    }
}