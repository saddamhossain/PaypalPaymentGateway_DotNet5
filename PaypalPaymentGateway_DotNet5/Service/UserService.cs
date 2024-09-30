using PaypalPaymentGateway_DotNet5.Context;
using PaypalPaymentGateway_DotNet5.Interface;
using PaypalPaymentGateway_DotNet5.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaypalPaymentGateway_DotNet5.Service
{
    public class UserService : IUser
    {
        private readonly DatabaseContext _context;
        public UserService(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            var result = _context.Users.ToList();
            return result;
        }

        public User GetById(Guid id)
        {
            var result = _context.Users.Where(s => s.Id == id).FirstOrDefault();
            return result;
        }

        public void InsertUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
