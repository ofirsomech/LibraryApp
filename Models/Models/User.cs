using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
  public class User
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public UserTypes Type { get; set; } = UserTypes.User;
    }
}
