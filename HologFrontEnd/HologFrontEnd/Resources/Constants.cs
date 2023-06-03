namespace HologFrontEnd.Resources
{
    public class Constants
    {
        //backEnd constants
        public const string webServiceUri = "http://192.168.1.51:8080";
        public const string user = "/user";
        public static string userId = "/" + App.DeviceId;
        public const string catEndpoint = "/categories";
        public const string itemEndpoint = "/items";
        public static string getAllItems = webServiceUri + user + userId + itemEndpoint;
        public static string getAllCategories = webServiceUri + user + userId + catEndpoint;
        //movies and series constants
        public const string token = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI2YWZhOWU1MTI1OTBmYmI3NzJiNjA0ZGY0NjMwZTRlNCIsInN1YiI6IjY0NjM0ZmQyOGM0NGI5NzgwOTYxYTY5YiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.LxHqdHj0THqsYnnYxnspA-t-0l6IgfAqsF38IIPjVuM";
        public const string queryParams = "&include_adult=false&language=en-US&page=1";
        public const string seriesEndPoint = "/tv?query=";
        public const string movieEndPoint = "/movie?query=";
        public const string baseImageUri = "https://image.tmdb.org/t/p/w200";
        //Videogames constants
        public const string videogamesAPIkey = "?key=0560241b043c43f3aa29bd3a2c360539";
        public const string gameParam1 = "&search="; 
        public const string gameParam2 = "&search_precise=true";

        //Books constants
        public const string bookParam = "books/v1/volumes?AIzaSyBtL63Iu1lEIUIp59ecpjLlLth7zYK5B_w&q=";
    }
}
