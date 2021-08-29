using Dishantha.Application.Common.Requests;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dishantha.Application.Sightings.Requests
{
    public class UpdateSightingRequest : BaseUpdateRequest
    {
        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Registration { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public DateTime DateAndTime { get; set; }

        public string AircraftPhoto { get; set; }
    }
}
