using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace web_lab1_fandom.Models
{
    public class Actors
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "This field is required")]
        public string Photo { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Birthdate")]
        //[Range(1,DateTime.UtcNow)]
        public DateTime Birthdate { get; set; }
        [Display(Name = "Information")]
        public string Info { get; set; }
        public virtual ICollection<Casts> Cast { get; set; }
    }
}
