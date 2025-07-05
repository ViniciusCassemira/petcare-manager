using System.ComponentModel.DataAnnotations.Schema;

namespace system_petshop.Models
{
    public abstract class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PassHash { get; set; }
    }
}
