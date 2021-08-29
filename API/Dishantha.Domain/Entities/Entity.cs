using System;
using System.ComponentModel.DataAnnotations;

namespace Dishantha.Domain.Entities
{
    public class Entity<TKey> : IEntity
    {
        public int Id { get; set; }

        public int? CreatedUser { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTimeOffset CreatedDateTime { get; set; } = DateTime.Now;

        public DateTimeOffset? UpdatedDateTime { get; set; }        

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
