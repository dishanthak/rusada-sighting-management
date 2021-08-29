using Dishantha.Application.Sightings.DTOs;
using Dishantha.Application.Sightings.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dishantha.Application.Sightings.Services
{
    public interface ISightingService
    {
        public Task<List<SightingDTO>> GetAllAsync();

        public Task<List<SightingDTO>> Search(SearchSightingRequest request);

        public Task<SightingDTO> GetByIdAsync(int id);

        public Task<SightingDTO> AddAsync(CreateSightingRequest request);

        public Task<SightingDTO> UpdateAsync(UpdateSightingRequest request);

        public Task<bool> DeleteAsync(int id);
        
    }
}
