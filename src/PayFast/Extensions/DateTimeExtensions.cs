namespace System
{
    public static class DateTimeExtensions
    {
        public static string ToPayFastDate(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        public static string ToPayFastDate(this DateTime? dateTime)
        {
            if(dateTime.HasValue)
            {
                return dateTime.Value.ToString("yyyy-MM-dd");
            }

            return string.Empty;
        }
    }
}
