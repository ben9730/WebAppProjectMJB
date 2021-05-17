using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppProjectMJB.Models
{
    public class Accessories
    {

        public int Id { get; set; }
        [Required]
        [Display(Name = "Accesory Name")]
        [StringLength(maximumLength:30,MinimumLength =2,ErrorMessage ="Accesory need to be more then two letters")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [StringLength(maximumLength: 1000, MinimumLength = 2, ErrorMessage = "write a summary")]
        public string Summary { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Accesory Image")]
        public string Image { get; set; }

        [Display(Name = "Console Name")]
        public int ConsoleId { get; set; }
        public Console Console { get; set; }

    }
}
