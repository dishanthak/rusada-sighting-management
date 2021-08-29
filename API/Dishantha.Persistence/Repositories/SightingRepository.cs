using Dishantha.Domain.Entities;
using Dishantha.Domain.Repositories;

namespace Dishantha.Persistence.Repositories
{
    public class SightingRepository : Repository<Sighting, int>, ISightingRepository
    {
        public SightingRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

    }
}
