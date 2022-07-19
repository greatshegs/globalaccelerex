namespace WebApplication1
{
    public class Root
    {
        public OpeningHours[] Monday { get; set; }
        public OpeningHours[] Tuesday { get; set; }
        public OpeningHours[] Wednesday { get; set; }
        public OpeningHours[] Thursday { get; set; }
        public OpeningHours[] Friday { get; set; }
        public OpeningHours[] Saturday { get; set; }
        public OpeningHours[] Sunday { get; set; }
    }
    public class OpeningHours
    {
        public string Type { get; set; }
        public int Value { get; set; }
    }

    public static class OpeningType
    {
        public const string open = "open";
        public const string close = "close";
    }

    public enum DaysOfTheWeek
    {
        Monday = 0,
        Tuesday = 1,
        Wednesday = 2,
        Thursday = 3,
        Friday = 4,
        Saturday = 5,
        Sunday = 6
    }
}
