using Dishantha.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Dishantha.Persistence.Configurations
{
    public class SightingConfiguration : IEntityTypeConfiguration<Sighting>
    {
        public void Configure(EntityTypeBuilder<Sighting> builder)
        {
            builder.Property(x => x.RowVersion).IsRowVersion();
            builder.ToTable("Sightings");

            // Seed
            builder.HasData(
                new Sighting
                {
                    Id = 1,
                    Make = "AD Scout",
                    Model = "777-300ER",
                    Registration = "G-RNAC",
                    Location = "London Gatwick",
                    DateAndTime = new DateTime(2010,8,28,8,20,0),
                    CreatedDateTime = DateTime.Now,
                    AircraftPhotoName = "b56b02aa-385d-42f2-8809-5f8d80c9ae71.png"
                },
                new Sighting
                {
                    Id = 2,
                    Make = "Aerfer Ariete",
                    Model = "777-302ER",
                    Registration = "G-RNAC",
                    Location = "London Gatwick",
                    DateAndTime = new DateTime(2010, 8, 28, 8, 25, 0),
                    CreatedDateTime = DateTime.Now,
                    AircraftPhotoName = "38dc45b2-0126-4657-9869-a1f8860b274f.png"
                },
                new Sighting
                {
                    Id = 3,
                    Make = "Beardmore W.B.V",
                    Model = "777-303ER",
                    Registration = "G-RNAC",
                    Location = "London Gatwick",
                    DateAndTime = new DateTime(2010, 8, 28, 8, 30, 0),
                    CreatedDateTime = DateTime.Now,
                    AircraftPhotoName = "bf98f2a3-d7c6-45c6-9481-a9ee844075f5.png"
                },
                new Sighting
                {
                    Id = 4,
                    Make = "Dewoitine D.25",
                    Model = "777-304ER",
                    Registration = "G-RNAC",
                    Location = "London Gatwick",
                    DateAndTime = new DateTime(2010, 8, 28, 8, 40, 0),
                    CreatedDateTime = DateTime.Now,
                    AircraftPhotoName = "40ec6d2f-3bf3-4225-9c14-1d7b99096129.png"
                }

            );

        }
    }
}
