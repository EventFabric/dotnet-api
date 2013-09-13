namespace eventfabric.api
{
    public class Session
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }

        public Session()
        {

        }
        public Session(string username, string password, string email)
        {
            this.username = username;
            this.password = password;
            this.email = email;
        }

    }
}