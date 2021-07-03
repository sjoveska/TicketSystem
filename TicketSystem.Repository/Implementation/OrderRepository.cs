using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TicketSystem.Data;
using TicketSystem.Domain.DomainModels;
using TicketSystem.Repository.Interface;

namespace TicketSystem.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }


        public List<Order> getAllOrders()
        {
            return entities
                .Include(z => z.User)
                .Include(z => z.TicketInOrders)
                .Include("TicketInOrders.SelectedTicket")
                .ToListAsync().Result;
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return entities
               .Include(z => z.User)
               .Include(z => z.TicketInOrders)
               .Include("TicketInOrders.SelectedTicket")
               .SingleOrDefaultAsync(z => z.Id == model.Id).Result;
        }

        public Order getOrderDetails(BaseEntity model)
        {
            throw new NotImplementedException();
        }

        List<Order> IOrderRepository.getAllOrders()
        {
            throw new NotImplementedException();
        }
    }
}
