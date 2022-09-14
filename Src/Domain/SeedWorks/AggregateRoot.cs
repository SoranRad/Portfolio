using System;

namespace Domain.SeedWork
{
	public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot
	{
		protected AggregateRoot() : base()
		{
			_domainEvents =
				new System.Collections.Generic.List<IDomainEvent>();
		}

		
		private readonly System.Collections.Generic.List<IDomainEvent> _domainEvents;

		[System.Text.Json.Serialization.JsonIgnore]
		public System.Collections.Generic.IReadOnlyList<IDomainEvent> DomainEvents
		{
			get
			{
				return _domainEvents;
			}
		} 

		protected void RaiseDomainEvent(IDomainEvent domainEvent)
		{
			if (domainEvent is null)
			{
				return;
			}

			_domainEvents?.Add(domainEvent);

		}

		protected void RemoveDomainEvent(IDomainEvent domainEvent)
		{
			if (domainEvent is null)
			{
				return;
			}

			_domainEvents?.Remove(domainEvent);
		}

		public void ClearDomainEvents()
		{ 
			_domainEvents?.Clear();
        }
	}
}
