using PaypalPaymentGateway_DotNet5.Context;
using PaypalPaymentGateway_DotNet5.Interface;
using PaypalPaymentGateway_DotNet5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaypalPaymentGateway_DotNet5.Service
{
    public class PaymentService : IPayment
    {
        private readonly DatabaseContext _context;
        public PaymentService(DatabaseContext context)
        {
            _context = context;
        }
        public IEnumerable<Payment> GetAll()
        {
            var result = _context.Payments.ToList();
            return result;
        }

        public Payment GetById(Guid id)
        {
            var result = _context.Payments.Where(s => s.Id == id).FirstOrDefault();
            return result;
        }

        public void InsertPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }
    }
}
