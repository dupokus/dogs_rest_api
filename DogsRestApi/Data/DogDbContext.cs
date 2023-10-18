using DogsRestApi.Model;
using DogsRestApi.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DogsRestApi.Data
{
    public class DogDbContext : DbContext, IApplicationDbContext, IUnitOfWork
    {
        protected readonly IConfiguration Configuration;
        private readonly IPublisher _publisher;

        public DogDbContext(DbContextOptions options, IConfiguration configuration, IPublisher publisher)
            :base(options)
        {
            Configuration = configuration;
            _publisher = publisher;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("DogApiDb"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DogDbContext).Assembly);
        }
        public DbSet<Dog> Dogs { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var domainEvents = ChangeTracker.Entries<Entity>()
                .Select(e => e.Entity)
                .Where(e => e.GetDomainEvents().Any())
                .SelectMany(e => e.GetDomainEvents());

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }

            return result;
        }
    }
}
