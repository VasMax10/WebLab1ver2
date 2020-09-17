using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_lab1_fandom.Models
{
    public class Series
    {
        public int ID { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }
        [Display(Name = "Poster")]
        //[Required(ErrorMessage = "This field is required")]
        public string Poster { get; set; }
        [Display(Name = "First Aired")]
        [Required(ErrorMessage = "This field is required")]
        [Range(1900,2020, ErrorMessage ="Year must be valid")]
        public int Premiere { get; set; }
        [Display(Name = "Is Ended")]
        [Required(ErrorMessage = "This field is required")]
        public bool IsEnded { get; set; }
        [Display(Name = "Background image")]
        //[Required(ErrorMessage = "This field is required")]
        public string BackImage { get; set; }
        [Display(Name = "Main color")]
        //[Required(ErrorMessage = "This field is required")]
        
        public string MainColor {get; set;}
        [Display(Name = "Secondary color")]
        //[Required(ErrorMessage = "This field is required")]
        public string SecondColor { get; set; }
        [Display(Name = "Information")]
        public string Info { get; set; }
        //public virtual ICollection<Casts> Cast { get; set; }
        public virtual ICollection<Characters> Characters { get; set; }
    }
}
