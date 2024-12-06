namespace Systems.Parsers.Object
{
    public class ObjectToInt : ObjectCaster<ObjectToInt, int>
    {
        protected override bool Parse(object valueToParse, out int result)
        {
            if (int.TryParse(valueToParse.ToString(), out result))
            {
                return true;
            }
            result = -1;
            return false;
        }
    }
}