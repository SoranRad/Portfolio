using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SeedWork
{
    public interface IAggregateRoot : IEntity
    {
        void ClearDomainEvents();

        IReadOnlyList<IDomainEvent> DomainEvents { get; }
    }
}
