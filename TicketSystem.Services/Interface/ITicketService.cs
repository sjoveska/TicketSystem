using System;
using System.Collections.Generic;
using System.Text;
using TicketSystem.Domain.DomainModels;
using TicketSystem.Models.DTO;

namespace TicketSystem.Services.Interface
{
    public interface ITicketService
    {
        List<Ticket> GetAllTickets();
        Ticket GetDetailsForTicket(Guid? id);
        void CreateNewTicket(Ticket t);
        void UpdeteExistingTicket(Ticket t);
        AddToCartDto GetCartInfo(Guid? id);
        void DeleteTicket(Guid id);
        bool AddToCart(AddToCartDto item, string userID);
    }
}
