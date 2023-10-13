using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DogsRestApi.Data
{
    public class DogDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DogDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("DogApiDb"));
        }

        public DbSet<Dog> Dogs { get; set; }
    }
}
