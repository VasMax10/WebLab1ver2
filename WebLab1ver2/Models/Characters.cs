using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Characters
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Birthyear")]
        [Range(0,10000, ErrorMessage ="Year must be valid")]
        public int Birthyear { get; set; }
        //[Required(ErrorMessage = "This field is required")]
        [Display(Name = "Photo")]
        public string Photo { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Status")] // alive, dead, etc.
        public string Status { get; set; }
        //[Required(ErrorMessage = "This field is required")]
        [Display(Name = "Information")]
        public string Info { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Series")]
        public int SeriesID { get; set; }
        public virtual Series Series { get; set; }
        public virtual ICollection<Casts> Cast { get; set; }
    }
}
