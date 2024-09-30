using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayPal.Api;
using PaypalPaymentGateway_DotNet5.Context;
using PaypalPaymentGateway_DotNet5.Interface;
using PaypalPaymentGateway_DotNet5.Models;
using PaypalPaymentGateway_DotNet5.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaypalPaymentGateway_DotNet5.Controllers
{
    public class SinglePaymentController : Controller
    {
        private readonly PayPal.Api.Payment Payment;
        private readonly IBook _bookService;
        private readonly IUser _userService;
        private readonly IPayment _paymentService;
        private readonly DatabaseContext _context;

        public SinglePaymentController(IBook bookService, IUser userService, IPayment paymentService, DatabaseContext context)
        {
            _bookService = bookService;
            _userService = userService;
            _paymentService = paymentService;
            _context = context;
        }
       

       // [HttpPost]
        public IActionResult PurchaseWithSinglePayment(Guid id)
        {
            var user = new User()
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "test@test.com"
            };
            _userService.InsertUser(user);
            var current_user_id = user.Id;

            var book = _bookService.GetById(id);
            var current_payment_id = new Guid();
            // Get PayPal API Context using configuration from web.config
            APIContext apicontext = PaypalConfiguration.GetAPIContext();

            // Create a new payment object
            var payment = new PayPal.Api.Payment
            {
                experience_profile_id = "XP-NB4L-Z9NM-ZA49-YZVM", // Created in the WebExperienceProfilesController. This one is for DigitalGoods.
                intent = "sale",
                payer = new Payer
                {
                    payment_method = "paypal"
                },
                transactions = new List<Transaction>
                    {
                        new Transaction
                        {
                           // invoice_number = "#123456789",
                            description = $"Landing Project's (Single Payment) for {DateTime.Now}. Transaction Description",
                            amount = new Amount
                            {
                                currency = "USD",
                                total = (book.Price).ToString(), // PayPal expects string amounts, eg. "20.00"
                            },
                            item_list = new ItemList()
                            {
                                items = new List<Item>()
                                {
                                    new Item()
                                    {
                                        description = $"Computer Buy from Landaing Project with (Single Payment) for {DateTime.Now}",
                                        currency = "USD",
                                        quantity = 1.ToString(),
                                        price = (book.Price).ToString(), // PayPal expects string amounts, eg. "20.00"  
                                        sku = "sku",
                                    }
                                }
                            }
                        }
                    },

                redirect_urls = new RedirectUrls
                {

                    return_url = Url.Action("Return", "SinglePayment", null, Url.ActionContext.HttpContext.Request.Scheme),
                    cancel_url = Url.Action("Cancel", "SinglePayment", null, Url.ActionContext.HttpContext.Request.Scheme)
                }
            };

            // Send the payment to PayPal
            var createdPayment = payment.Create(apicontext);

            // Save a reference to the paypal payment
            Models.Payment obj_payment = new Models.Payment();
            obj_payment.UserId = current_user_id;
            obj_payment.BookId = new Guid();
            obj_payment.Quantity = 1;
            obj_payment.Amount = book.Price;
            obj_payment.TotalAmount = book.Price;
            obj_payment.PaymentDate = DateTime.Now;
            obj_payment.Currency = "USD";
            obj_payment.Intent = payment.intent;
            obj_payment.PayPalReference = createdPayment.id;
            obj_payment.PayerId = null;
            obj_payment.InvoiceNumber = "#1234";
            obj_payment.Description = "Product des";
            obj_payment.ShippingAddress = "Dhaka, Bangladesh";
            obj_payment.Tax = "1";
            obj_payment.PaymentMethod = "Paypal";

            _paymentService.InsertPayment(obj_payment);

            current_payment_id = obj_payment.Id;

            TempData["current_payment_id"] = current_payment_id;
            // Find the Approval URL to send our user to
            var approvalUrl = createdPayment.links.FirstOrDefault(s => s.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase));

            // Send the user to PayPal to approve the payment
            return Redirect(approvalUrl.href);

        }

        public ActionResult Return(string payerId, string paymentId)
        {
            var current_payment = TempData["current_payment_id"].ToString();
            // Fetch the existing user info
            var info = _context.Payments.FirstOrDefault(s => s.PayPalReference == paymentId);
            var current_payment_id_value = _context.Payments.FirstOrDefault(s => Convert.ToString(s.Id) == current_payment);

            // Get PayPal API Context using configuration from web.config
            APIContext apicontext = PaypalConfiguration.GetAPIContext();

            // Set the payer for the payment
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };

            // Identify the payment to execute
            var payment = new PayPal.Api.Payment()
            {
                id = paymentId
            };
            current_payment_id_value.PayerId = payerId;
            _context.Entry(current_payment_id_value).State = EntityState.Modified;
            _context.SaveChanges();

            // Execute the Payment
            var executedPayment = payment.Execute(apicontext, paymentExecution);

            return RedirectToAction("ThankYou");
        }


        [HttpGet]
        public ActionResult Cancel()
        {
            return View();
        }


        [HttpGet]
        public ActionResult ThankYou()
        {
            return View();
        }

    }
}
