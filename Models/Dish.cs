using System.ComponentModel.DataAnnotations;
using System;

namespace CRUDelicious.Models
{
    public class Dish
    {
        [Key]
        public int DishId {get;set;}
        [Required(ErrorMessage="Name of Dish is Required")]
        public string Name {get;set;}
        [Required(ErrorMessage="Chef's Name is Required")]
        public string Chef {get;set;}
        [Required(ErrorMessage="Tastiness is Required")]
        public int? Tastiness {get;set;}
        [Required(ErrorMessage="# of Calories is Required")]
        [Range(0,5000, ErrorMessage="Calories must be more than 0")]
        public int? Calories {get;set;}
        [Required(ErrorMessage="Description is Required")]
        public string  Description {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}