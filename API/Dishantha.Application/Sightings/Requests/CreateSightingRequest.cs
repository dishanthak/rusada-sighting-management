using System;
using System.ComponentModel.DataAnnotations;

namespace Dishantha.Application.Sightings.Requests
{
    public class CreateSightingRequest
    {
        [Required]
        [StringLength(128, ErrorMessage = "Maximum length cannot exceed 128 Characters")]
        public string Make { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "Maximum length cannot exceed 128 Characters")]
        public string Model { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]{1,2}-[a-zA-Z]{1,5}$", ErrorMessage = "Missed match the pattern")]
        public string Registration { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Maximum length cannot exceed 255 Characters")]
        public string Location { get; set; }

        [Required]
        public DateTime DateAndTime { get; set; }

        public string AircraftPhoto { get; set; }
    }
}
