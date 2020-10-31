using EFxceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace OtripleS.Web.Api.Brokers.Storage
{
    public class StorageBroker : EFxceptionsContext, IStorageBroker
    {
        private readonly IConfiguration _configuration;
        public StorageBroker(IConfiguration configuration)
        {
            _configuration = configuration;
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
