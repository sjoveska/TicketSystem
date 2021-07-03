using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketSystem.Domain.DomainModels;
using TicketSystem.Models.DTO;
using TicketSystem.Repository.Interface;
using TicketSystem.Services.Interface;

namespace TicketSystem.Services.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<TicketInShoppingCart> _ticketInCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<TicketService> _logger;
        public TicketService(IRepository<Ticket> ticketRepository, ILogger<TicketService> logger, IRepository<TicketInShoppingCart> ticketInCartRepository, IUserRepository userRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _ticketInCartRepository = ticketInCartRepository;
            _logger = logger;
        }

        public bool AddToCart(AddToCartDto item, string userID)
        {

            var user = this._userRepository.Get(userID);

            var userShoppingCard = user.UserCart;

            if (item.TicketId != null && userShoppingCard != null)
            {
                var ticket = this.GetDetailsForTicket(item.TicketId);

                if (ticket != null)
                {
                    TicketInShoppingCart itemToAdd = new TicketInShoppingCart
                    {
                        Id = Guid.NewGuid(),
                        Ticket = ticket,
                        TicketId = ticket.Id,
                        Cart = userShoppingCard,
                        CartId = userShoppingCard.Id,
                        quantity = item.Quantity
                    };

                    this._ticketInCartRepository.Insert(itemToAdd);
                    _logger.LogInformation("Ticket was successfully added into Cart");
                    return true;
                }
                return false;
            }
            _logger.LogInformation("Something was wrong. TicketId or UserCard may be unaveliable!");
            return false;
        }

        public void CreateNewTicket(Ticket t)
        {
            this._ticketRepository.Insert(t);
        }

        public void DeleteTicket(Guid id)
        {
            var product = this.GetDetailsForTicket(id);
            this._ticketRepository.Delete(product);
        }

        public List<Ticket> GetAllTickets()
        {
            _logger.LogInformation("GetAllProducts was called!");
            return this._ticketRepository.GetAll().ToList();
        }

        public Ticket GetDetailsForTicket(Guid? id)
        {
            return this._ticketRepository.Get(id);
        }

        public AddToCartDto GetCartInfo(Guid? id)
        {
            var ticket = this.GetDetailsForTicket(id);
            AddToCartDto model = new AddToCartDto
            {
                SelectedTicket = ticket,
                TicketId = ticket.Id,
                Quantity = 1
            };

            return model;
        }

        public void UpdeteExistingTicket(Ticket t)
        {
            this._ticketRepository.Update(t);
        }

        List<Ticket> ITicketService.GetAllTickets()
        {
            return this._ticketRepository.GetAll().ToList();
        }

        Ticket ITicketService.GetDetailsForTicket(Guid? id)
        {
            return this._ticketRepository.Get(id);
        }
    }
}
