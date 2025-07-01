using Contracts.Domains.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Domains
{
    public abstract class EntityAuditBase<T> : EntityBase<T>, IAuditable
    {
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        protected EntityAuditBase()
        {
            CreatedDate = DateTimeOffset.UtcNow;
        }
        protected EntityAuditBase(T id) : base(id)
        {
            CreatedDate = DateTimeOffset.UtcNow;
        }
    }
}
