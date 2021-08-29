using System;

namespace Dishantha.Domain.Entities
{
    public class Sighting : Entity<int>
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public string Registration { get; set; }

        public string Location { get; set; }

        public DateTime DateAndTime { get; set; }

        public string AircraftPhotoName { get; set; }
        
    }
}
