using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Resourse.Models;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Resourse.Controllers
{
    [Route("books")]
    public class BooksController : Controller
    {
        private readonly BookRepository _bookRepository;

        public BooksController(BookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }

        [HttpGet]
        public IActionResult GetAvailableBooks()
        {
            return Ok(_bookRepository.Books);
        }
    }
}
