using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieMvcProject.Models
{
    [MetadataType(typeof(MoviesMetadata))]
    public partial class Movie
    {

    }

    public class MoviesMetadata
    {

        [Display(Name = "Movie Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Movie Name Requierd")]
        [StringLength(25, ErrorMessage = "Max lenght is 25 and Minimum is 3", MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Director Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Director Name Requierd")]
        [StringLength(25, ErrorMessage = "Max lenght is 25 and Minimum is 3", MinimumLength = 3)]
        public string DirectorName { get; set; }

        [Display(Name = "Descripton")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Descripton Requierd")]
        [StringLength(500, ErrorMessage ="Max lenght is 500 and Minimum is 3",MinimumLength = 3)]
        public string Description { get; set; }

        [Display(Name = "Add Image")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Image Required")]
        public string ImagePath { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Year Required")]
        public Nullable<int> Year { get; set; }


        public int GenreId { get; set; }
    }
}