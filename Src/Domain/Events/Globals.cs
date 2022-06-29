namespace ONLINE_SHOP.Domain.Events;

public static class Globals
{
    public static class Channels
    {
        public const string WeatherChannel = "WEATHER";
    }

    public static class Events
    {
        public static class Routes
        {
            public const string OrderRoute = "ICX_ORDER";
        }

        public const string NotificationsBus = "NOTIFICATIONS";
        public const string StateChangesBus = "STATE_CHANGES";
    }
}