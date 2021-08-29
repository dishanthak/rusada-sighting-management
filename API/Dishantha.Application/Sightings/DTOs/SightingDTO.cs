using Dishantha.Application.Common.DTOs;
using System;


namespace Dishantha.Application.Sightings.DTOs
{
    public class SightingDTO : BaseDTO
    {        
        
        public string Make { get; set; }

        public string Model { get; set; }

        public string Registration { get; set; }

        public string Location { get; set; }

        public DateTime DateAndTime { get; set; }

        public string AircraftPhotoName { get; set; }

        public string AircraftPhoto { get; set; }
        
    }
}
