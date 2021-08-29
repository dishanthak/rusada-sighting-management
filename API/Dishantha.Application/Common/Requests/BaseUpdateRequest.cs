using System.ComponentModel.DataAnnotations;

namespace Dishantha.Application.Common.Requests
{
    public class BaseUpdateRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public byte[] RowVersion { get; set; }
    }
}
