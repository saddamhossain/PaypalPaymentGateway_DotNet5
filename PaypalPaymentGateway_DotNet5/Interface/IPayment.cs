using PaypalPaymentGateway_DotNet5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaypalPaymentGateway_DotNet5.Interface
{
    public interface IPayment
    {
        IEnumerable<Payment> GetAll();
        Payment GetById(Guid id);
        void InsertPayment(Payment payment);
    }
}
