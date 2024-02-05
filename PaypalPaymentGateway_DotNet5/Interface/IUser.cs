using PaypalPaymentGateway_DotNet5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaypalPaymentGateway_DotNet5.Interface
{
    public interface IUser
    {
        IEnumerable<User> GetAll();
        User GetById(Guid id);
        void InsertUser(User user);
    }
}
