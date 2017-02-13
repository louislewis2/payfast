namespace System
{
    public static class ObjectExtensions
    {
        public static T ToEnum<T>(this object value)
        {
            T enumVal = (T)Enum.Parse(typeof(T), value.ToString());
            return enumVal;
        }
    }
}
