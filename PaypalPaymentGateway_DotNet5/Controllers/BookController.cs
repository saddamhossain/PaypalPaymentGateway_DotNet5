using Microsoft.AspNetCore.Mvc;
using PaypalPaymentGateway_DotNet5.Interface;
using System;

namespace PaypalPaymentGateway_DotNet5.Controllers
{
    public class BookController : Controller
    {
        private readonly IBook _bookService;
        public BookController(IBook bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = _bookService.GetAll();
            return View(result);
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var result = _bookService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

    }
}
