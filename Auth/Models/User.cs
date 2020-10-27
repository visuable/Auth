using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    public class User
    {
        [Key]
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Salt { get; set; }
    }
}
