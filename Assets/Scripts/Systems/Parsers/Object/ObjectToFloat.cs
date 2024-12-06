namespace Systems.Parsers.Object
{
    public class ObjectToFloat : ObjectCaster<ObjectToFloat, float>
    {
        protected override bool Parse(object valueToParse, out float result)
        {
            if (float.TryParse(valueToParse.ToString(), out result))
            {
                return true;
            }
            result = -1F;
            return false;
        }
    }
}