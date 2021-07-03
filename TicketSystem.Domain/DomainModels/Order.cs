using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Models.Identity;

namespace TicketSystem.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public TicketSystemApplicationUser User { get; set; }
        public virtual ICollection<TicketInOrder> TicketInOrders { get; set; }
    }
}
