using Dishantha.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Dishantha.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public UnitOfWork(
            ApplicationDbContext context,
            ILoggerFactory loggerFactory
        )
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
