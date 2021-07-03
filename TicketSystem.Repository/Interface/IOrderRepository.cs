using System;
using System.Collections.Generic;
using System.Text;
using TicketSystem.Domain.DomainModels;

namespace TicketSystem.Repository.Interface
{
    public interface IOrderRepository
    {
        List<Order> getAllOrders();
        Order GetOrderDetails(BaseEntity model);
    }
}
