using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rentor.Models;

namespace Rentorer.ViewModels
{
    public class NewMovieViewModel
    {
        public Movie Movie { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public bool? IsEditing { get; set; }

    }
}