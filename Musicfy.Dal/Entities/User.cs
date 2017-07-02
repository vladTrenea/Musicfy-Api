using System.Security.AccessControl;

namespace Musicfy.Dal.Entities
{
    public class User
    {
        public string id { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public bool isAdmin { get; set; }
    }
}