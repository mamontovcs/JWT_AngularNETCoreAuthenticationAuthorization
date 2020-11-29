using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth.Resourse.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Resourse.Controllers
{
    [Route("orders")]
    public class OrdersController : Controller
    {
        private readonly BookRepository _bookRepository;

        private long UserId => long.Parse(User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);

        public OrdersController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult GetOrders()
        {
            if (!_bookRepository.Orders.ContainsKey(UserId))
            {
                return Ok(Enumerable.Empty<Book>());
            }

            var orderedBookIds = _bookRepository.Orders.Single(x => x.Key == UserId).Value;
            var orderedBooks = _bookRepository.Books.Where(x => orderedBookIds.Contains(x.Id));

            return Ok(orderedBooks);
        }
    }
}
