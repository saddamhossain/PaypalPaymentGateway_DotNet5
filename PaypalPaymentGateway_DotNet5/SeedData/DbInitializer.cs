using Microsoft.EntityFrameworkCore;
using PaypalPaymentGateway_DotNet5.Context;
using PaypalPaymentGateway_DotNet5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaypalPaymentGateway_DotNet5.SeedData
{
    public class DbInitializer : IDbInitializer
    {
        private readonly DatabaseContext _context;

        public DbInitializer(DatabaseContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            try
            {
                // If any migration pending, then it will migrate that. 
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {
                throw;
            }

            if (!_context.Books.Any())
            {
                var _books = new List<Book>()
                {
                    new Book()
                    {
                        Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                        Title="Managing Oneself",
                        Description="We live in an age of unprecedented opportunity: with ambition, drive, and talent, you can rise to the top of your chosen profession, regardless of where you started out...",
                        Author= "Peter Ducker",
                        Thumbnail = @"\images\1.jpg",
                        Price = 19.90
                    },
                    new Book()
                    {
                        Id= new Guid("117366b8-3541-4ac5-8732-860d698e26a2"),
                        Title="Evolutionary Psychology",
                        Description="Evolutionary Psychology: The New Science of the Mind, 5th edition provides students with the conceptual tools of evolutionary psychology, and applies them to empirical research...",
                        Author= "David Buss",
                        Thumbnail = @"\images\2.jpg",
                        Price = 29.90
                    },
                    new Book()
                    {
                        Id= new Guid("66ff5116-bcaa-4061-85b2-6f58fbb6db25"),
                        Title="How to Win Friends & Influence People",
                        Description="Millions of people around the world have improved their lives based on the teachings of Dale Carnegie. In How to Win Friends and Influence People, he offers practical advice...",
                        Author= "Dale Carnegie",
                        Thumbnail = @"\images\3.jpg",
                        Price = 32.49
                    },
                    new Book()
                    {
                        Id =  new Guid("cd5089dd-9754-4ed2-b44c-488f533243ef"),
                        Title = "The Selfish Gene",
                        Description = "Professor Dawkins articulates a gene’s eye view of evolution. A view giving center stage to these persistent units of information, and in which organisms can be seen as...",
                        Author = "Richard Dawkins",
                        Thumbnail = @"\images\4.jpg",
                        Price = 17.89
                    },
                    new Book()
                    {
                        Id =  new Guid("d81e0829-55fa-4c37-b62f-f578c692af78"),
                        Title = "The Lessons of History",
                        Description = "Will and Ariel Durant have succeeded in distilling for the reader the accumulated store of knowledge and experience from their five decades of work on the eleven monumental...",
                        Author = "Will & Ariel Durant",
                        Thumbnail = @"\images\5.jpg",
                        Price = 32.00
                    }
            };
                _context.Books.AddRange(_books);
                _context.SaveChangesAsync();
            }
        }

    }
}
