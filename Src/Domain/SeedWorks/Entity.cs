using System;

namespace Domain.SeedWork
{
    public interface IEntity
    {
        object Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        public bool IsDisable { get; set; }

    }

    public interface        IEntity<T> : IEntity
    {
        new T Id { get; set; }
    }

	public abstract class   Entity<T> : IEntity<T>
	{
		#region Static Member(s)
		public static bool operator ==(Entity<T> leftObject, Entity<T> rightObject)
		{
			if (leftObject is null && rightObject is null)
			{
				return true;
			}

			if (leftObject is null && !(rightObject is null))
			{
				return false;
			}

			if (!(leftObject is null) && rightObject is null)
			{
				return false;
			}

			return leftObject.Equals(rightObject);
		}
		public static bool operator !=(Entity<T> leftObject, Entity<T> rightObject)
		{
			return !(leftObject == rightObject);
		}
		#endregion /Static Member(s)

		protected Entity() : base()
		{
			 
		}
         
        public T Id { get; set; }
        private DateTime? createdDate = DateTime.Now;

        object IEntity.Id
        {
            get { return this.Id; }
            set { Id = (T)Convert.ChangeType(value, typeof(T)); }
        }
        
        public DateTime CreatedDate
        {
            get { return createdDate ?? DateTime.Now; }
            set { createdDate = DateTime.Now; }
        }
         
        public DateTime? ModifiedDate { get; set; }
        public bool IsDisable { get ; set ; }


        int? _requestedHashCode;

		public bool IsTransient()
		{
			return Id.Equals(default(T)) ;
		}

        public override bool Equals(object anotherObject)
        {
            if (anotherObject is null)
            {
                return false;
            }

            if (!(anotherObject is Entity<T>))
            {
                return false;
            }

            if (ReferenceEquals(this, anotherObject))
            {
                return true;
            }

            var anotherEntity = anotherObject as Entity<T>;

            // For EF Core!
            if (GetRealType() != anotherEntity.GetRealType())
            {
                return false;
            }

            if (GetType() == anotherEntity.GetType())
            {
                if (IsTransient() || anotherEntity.IsTransient())
                {
                    return false;
                }
                else
                {
                    return Id.Equals(anotherEntity.Id) ;
                }
            }

            return false;
        }

        public override int GetHashCode()
		{
			if (IsTransient() == false)
			{
				if (_requestedHashCode.HasValue == false)
				{
					_requestedHashCode = this.Id.GetHashCode() ^ 31;
				}

				// XOR for random distribution. See:
				// https://docs.microsoft.com/archive/blogs/ericlippert/guidelines-and-rules-for-gethashcode
				return _requestedHashCode.Value;
			}
			else
			{
				return base.GetHashCode();
			}
		}

		private System.Type GetRealType()
		{
			System.Type type = GetType();

			if (type.ToString().Contains("Castle.Proxies."))
			{
				return type.BaseType;
			}

			return type;
		}
	}
}
