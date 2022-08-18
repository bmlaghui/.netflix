using System;
using System.Collections.Generic;
using System.Text;

namespace netflix.Models
{
    public class Movie
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Plot { get; set; }

        private string metascore;
        public string Metascore
        {
            get { return $"{metascore}/100"; }
            set => metascore = value; 
        }
    }
}
