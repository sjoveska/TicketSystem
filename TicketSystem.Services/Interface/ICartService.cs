using System;
using System.Collections.Generic;
using System.Text;
using TicketSystem.Models.DTO;

namespace TicketSystem.Services.Interface
{
    public interface ICartService
    {
        ShoppingCartDto getCartInfo(string userId);
        bool deleteTicketFromCart(string userId, Guid id);
        bool orderNow(string userId);
    }
}
