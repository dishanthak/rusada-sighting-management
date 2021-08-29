using System;
using System.ComponentModel.DataAnnotations;

namespace Dishantha.Domain.Entities
{
    public interface IEntity
    {
        int Id { get; }

        DateTimeOffset CreatedDateTime { get; set; }

        DateTimeOffset? UpdatedDateTime { get; set; }

        public int? CreatedUser { get; set; }

        public int? UpdatedUser { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
