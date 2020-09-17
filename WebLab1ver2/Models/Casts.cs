using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_lab1_fandom.Models
{
    public class Casts
    {
        public int ID { get; set; }
        [Display(Name = "Actor")]
        [Required(ErrorMessage = "This field is required")]
        public int ActorID { get; set; }
        [Display(Name = "Character")]
        [Required(ErrorMessage = "This field is required")]
        public int CharacterID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int FirstAppereance { get; set; } // first season
        public int LastAppereance { get; set; } // last season
        public virtual Actors Actor { get; set; }
        public virtual Characters Character { get; set; }
    }
}
