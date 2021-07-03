using TicketSystem.Domain.DomainModels;
using TicketSystem.Repository.Interface;
using TicketSystem.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketSystem.Models.DTO;

namespace TicketSystem.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly IRepository<ShoppingCart> _cartRepositorty;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<TicketInOrder> _ticketInOrderRepository;
        private readonly IUserRepository _userRepository;
        //private readonly IRepository<EmailMessage> _mailRepository;

        public CartService(IRepository<ShoppingCart> cartRepositorty, IRepository<TicketInOrder> ticketInOrderRepository, IRepository<Order> orderRepositorty, IUserRepository userRepository, IRepository<EmailMessage> mailRepository)
        {
            _cartRepositorty = cartRepositorty;
            _userRepository = userRepository;
            _orderRepository = orderRepositorty;
            _ticketInOrderRepository = ticketInOrderRepository;
            //_mailRepository = mailRepository;
        }

        public bool deleteTicketFromCart(string userId, Guid id)
        {
            if (!string.IsNullOrEmpty(userId) && id != null)
            {

                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                var itemToDelete = userShoppingCart.TicketInShoppingCarts.Where(z => z.TicketId.Equals(id)).FirstOrDefault();

                userShoppingCart.TicketInShoppingCarts.Remove(itemToDelete);

                this._cartRepositorty.Update(userShoppingCart);

                return true;
            }

            return false;
        }

        public ShoppingCartDto getCartInfo(string userId)
        {
            var loggedInUser = this._userRepository.Get(userId);

            var userShoppingCart = loggedInUser.UserCart;

            var AllTickets = userShoppingCart.TicketInShoppingCarts.ToList();

            var allProductPrice = AllTickets.Select(z => new
            {
                ProductPrice = z.Ticket.Price,
                Quanitity = z.quantity
            }).ToList();

            double totalPrice = 0.0;


            foreach (var item in allProductPrice)
            {
                totalPrice += item.Quanitity * item.ProductPrice;
            }


            ShoppingCartDto cartDto = new ShoppingCartDto
            {
                Tickets = AllTickets,
                TotalPrice = totalPrice
            };


            return cartDto;

        }

        public bool orderNow(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {

                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                /*EmailMessage mail = new EmailMessage();
                mail.MailTo = loggedInUser.Email;
                mail.Subject = "Successfully created order";
                mail.Status = false;*/

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                this._orderRepository.Insert(order);

                List<TicketInOrder> ticketsInOrders = new List<TicketInOrder>();

                var result = userShoppingCart.TicketInShoppingCarts.Select(z => new TicketInOrder
                {
                    Id = Guid.NewGuid(),
                    TicketId = z.Ticket.Id,
                    SelectedTicket = z.Ticket,
                    OrderId = order.Id,
                    UserOrder = order,
                    Quantity = z.quantity
                }).ToList();

                StringBuilder sb = new StringBuilder();

                double totalPrice = 0.0;

                sb.AppendLine("Your order is completed. The order conains: ");

                for (int i = 1; i <= result.Count(); i++)
                {
                    var item = result[i - 1];
                    totalPrice += item.Quantity * item.SelectedTicket.Price;
                    sb.AppendLine(i.ToString() + ". " + item.SelectedTicket.Name + " with price of: " + item.SelectedTicket.Price + " and quantity of: " + item.Quantity);
                }

                sb.AppendLine("Total price: " + totalPrice.ToString());


                //mail.Content = sb.ToString();


                ticketsInOrders.AddRange(result);

                foreach (var item in ticketsInOrders)
                {
                    this._ticketInOrderRepository.Insert(item);
                }

                loggedInUser.UserCart.TicketInShoppingCarts.Clear();

                this._userRepository.Update(loggedInUser);
                // this._mailRepository.Insert(mail);

                return true;
            }
            return false;
        }

        ShoppingCartDto ICartService.getCartInfo(string userId)
        {
            throw new NotImplementedException();
        }
    }
}