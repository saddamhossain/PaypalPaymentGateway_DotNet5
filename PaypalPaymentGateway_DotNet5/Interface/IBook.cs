using PaypalPaymentGateway_DotNet5.Models;
using System;
using System.Collections.Generic;

namespace PaypalPaymentGateway_DotNet5.Interface
{
    public interface IBook
    {
        IEnumerable<Book> GetAll();
        Book GetById(Guid id);
    }
}
