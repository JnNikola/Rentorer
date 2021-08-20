using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using Rentor.Models;
using Rentorer.CustomValidations;


namespace Rentor.Models
{
    public class Movie
    {
        
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Genre Genre { get; set; }
        
        [Required]
        [Display(Name = "Genre")]
        public byte GenreId { get; set; }

        [Display(Name = "Release Day")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Date Added")]
        [Required] 
        public DateTime DateAdded { get; set; }

        [Display(Name = "Number in Stock")]
        [Required]
        [Range(1, 30)]
        public int NumberInStock { get; set; }
        

    }
}