using System;
using System.Collections.Generic;
using System.Text;
using TicketSystem.Domain.DomainModels;
using TicketSystem.Repository.Interface;
using TicketSystem.Services.Interface;

namespace TicketSystem.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        public List<Order> getAllOrders()
        {
            return this._orderRepository.getAllOrders();
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return this._orderRepository.GetOrderDetails(model);
        }

        public Order getOrderDetails(BaseEntity model)
        {
            throw new NotImplementedException();
        }

        List<Order> IOrderService.getAllOrders()
        {
            throw new NotImplementedException();
        }
    }
}
