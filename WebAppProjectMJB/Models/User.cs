using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppProjectMJB.Models
{
    public enum UserType
    {
        Client,
        Editor,
        Admin
    }

    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Username need to be more then one letter")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Password need to be more then 4 letter")]
        [RegularExpression("^[A-Z]+[0-9a-zA-Z]*$", ErrorMessage = "Password must start with Upper case,can contin lower case and numbers ,Exclude spaces and &@#א")]
        //מחייב סיסמה שמתחילה באות גדולה לפחות אחת ואז מספרים ואותיות חופשי
        public string Password { get; set; }


        [DataType(DataType.EmailAddress)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "email need to be more then 2 letter")]
        public string Email { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public string Address { get; set; }

        //defulte type for the user
        public UserType Type { get; set; } = UserType.Client;

        //try many to many, maybe delete this? and add order instad 
        public List<Game> Games { get; set; }

    }
}
