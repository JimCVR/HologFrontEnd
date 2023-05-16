namespace HologFrontEnd.Resources
{
    public class Constants
    {
        public const string webServiceUri = "http://192.168.1.51:8080";
        public const string user = "/user";
        public static string userId = "/" + App.DeviceId;
        public const string catEndpoint = "/categories";
        public const string itemEndpoint = "/items";
        //Ip de casa 192.168.1.48
        //IP de oficina 192.168.1.49
        public static string getAllItems = webServiceUri + user + userId + itemEndpoint;
        public static string getAllCategories = webServiceUri + user + userId + catEndpoint;
    }
}
