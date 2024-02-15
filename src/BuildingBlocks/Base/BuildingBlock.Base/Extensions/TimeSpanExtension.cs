namespace BuildingBlock.Base.Extensions
{
    public static class TimeSpanExtension
    {
        public static TimeSpan AddMinute(int minute)
            => TimeSpan.FromMinutes(minute);

        public static TimeSpan AddHours(int hours)
            => TimeSpan.FromHours(hours);

        public static TimeSpan AddSecond(int second)
            => TimeSpan.FromSeconds(second);
    }
}
