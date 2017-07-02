namespace Musicfy.Bll.Models
{
    public class UserAuthorizationModel
    {
        public string id { get; set; }

        public string username { get; set; }

        public string token { get; set; }

        public bool isAdmin { get; set; }
    }
}