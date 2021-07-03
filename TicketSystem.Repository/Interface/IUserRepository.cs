using System;
using System.Collections.Generic;
using System.Text;
using TicketSystem.Models.Identity;

namespace TicketSystem.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<TicketSystemApplicationUser> GetAll();
        TicketSystemApplicationUser Get(string id);
        void Insert(TicketSystemApplicationUser entity);
        void Update(TicketSystemApplicationUser entity);
        void Delete(TicketSystemApplicationUser entity);
    }
}
