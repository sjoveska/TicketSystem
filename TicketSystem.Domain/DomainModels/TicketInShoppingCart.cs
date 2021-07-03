using System;
using System.Collections.Generic;
using System.Text;

namespace TicketSystem.Domain.DomainModels
{
    public class TicketInShoppingCart : BaseEntity
    {
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public Guid CartId { get; set; }
        public ShoppingCart Cart { get; set; }
        public int quantity { get; set; }
    }
}
