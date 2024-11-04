namespace RecipeShare.Models {
    public class UserActivity {

        public int Id { get; set; }
        public string Data { get; set; }
        public string Url { get; set; }
        public string Username { get; set; }
        public string IpAddress { get; set; }

        public UserActivity () { }

        public UserActivity(string data, string url, string username, string ipAddress) {
            Data = data;
            Url = url;
            Username = username;
            IpAddress = ipAddress;
        }
    }
}
