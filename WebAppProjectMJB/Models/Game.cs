using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppProjectMJB.Models
{
    public class Game
    {

        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "game name need to be more then one letter")]
        [Display(Name = "Game Name")]
        
        public string Name { get; set; }
        [Required]
        [Range(0, 600)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [StringLength(maximumLength: 1000, MinimumLength = 2, ErrorMessage = "write a summary to the game")]
        public string Summary { get; set; }

        [Display(Name = "Cover Image")]
        [DataType(DataType.ImageUrl)]
        public string CoverImage { get; set; }

        [Url]
        // need to put /embed for Youtube vid
        public string Trailer { get; set; }

        [Display(Name = "Console")]
        public int GameConsoleId { get; set; }

        public GameConsole Console { get; set; }

        
    }

}
