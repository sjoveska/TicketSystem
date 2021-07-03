using System;
using System.Collections.Generic;
using System.Text;
using TicketSystem.Domain.DomainModels;

namespace TicketSystem.Services.Interface
{
    public interface IOrderService
    {
        List<Order> getAllOrders();
        Order GetOrderDetails(BaseEntity model);
    }
}
