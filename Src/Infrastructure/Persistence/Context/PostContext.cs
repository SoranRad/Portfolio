using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.SeedWork;
using Infrastructure.Persistence.Extention;
using Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context
{
    public class PostContext : DbContext
    {
        private readonly IMediator _mediator;
        private readonly AuditableEntitySaveChangesInterceptor _saveChangesInterceptor; 

        public PostContext
        (
            DbContextOptions<PostContext> options, 
            IMediator mediator,
            AuditableEntitySaveChangesInterceptor SaveChangesInterceptor

        ) : base(options)
        {
            _mediator = mediator;
            _saveChangesInterceptor = SaveChangesInterceptor; 
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityAssembly = typeof(IAggregateRoot).Assembly;

            modelBuilder.HasDefaultSchema                       ("General");
            modelBuilder.RegisterAllEntities<IAggregateRoot>    (entityAssembly);
            modelBuilder.ApplyConfigurationsFromAssembly        (this.GetType().Assembly);
            modelBuilder.AddSequentialGuidForIdConvention       (); 

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_saveChangesInterceptor);
        }

        public override int SaveChanges()
        {
            var affectedRows = base.SaveChanges();

            if (affectedRows > 0)
                Task.Run(async () => await PublishEvents(CancellationToken.None));

            return affectedRows;
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var affectedRows = base.SaveChanges(acceptAllChangesOnSuccess);

            if (affectedRows > 0)
                Task.Run(async () => await PublishEvents(CancellationToken.None));

            return affectedRows;
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var affectedRows = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            if (affectedRows > 0)
                await PublishEvents(cancellationToken);

            return affectedRows;
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var affectedRows = await base.SaveChangesAsync(cancellationToken);

            if (affectedRows > 0)
                await PublishEvents(cancellationToken);

            return affectedRows;
        }


        private async Task PublishEvents(CancellationToken cancellationToken)
        {
            var aggregateRoots = ChangeTracker
                .Entries()
                .Where(current => current.Entity is IAggregateRoot)
                .Select(current => current.Entity as IAggregateRoot)
                .ToList();

            foreach (var aggregateRoot in aggregateRoots)
            {
                // Dispatch Events!
                foreach (var domainEvent in aggregateRoot.DomainEvents)
                {
                    await _mediator.Publish(domainEvent, cancellationToken);
                }

                // Clear Events!
                aggregateRoot.ClearDomainEvents();
            }
        }
    }
}
