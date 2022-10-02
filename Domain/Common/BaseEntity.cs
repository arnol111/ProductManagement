using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        private readonly List<BaseEvents> _domainEvents = new List<BaseEvents>();

        [NotMapped]
        public IReadOnlyCollection<BaseEvents> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(BaseEvents domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(BaseEvents domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
