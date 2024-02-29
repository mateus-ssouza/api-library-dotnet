using ApiBiblioteca.Domain.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ApiBiblioteca.Application.Utils;
using ApiBiblioteca.Application.ViewModel;
using ApiBiblioteca.Infra.Repositories;

namespace ApiBiblioteca.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookRepository.GetAll();

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var book = await _bookRepository.GetById(id);

            return book == null ? NotFound("Book not found!") : Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BookViewModel viewModel)
        {
            var book = CreateUtil.BookCreate(viewModel);
            await _bookRepository.Add(book);

            return StatusCode(201, "Book registered successfully!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] BookViewModel viewModel, Guid id)
        {
            var book = CreateUtil.BookCreate(viewModel);
            await _bookRepository.Update(id, book);

            return Ok("Book updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _bookRepository.Delete(id);

            return Ok("Book removed successfully!");
        }
    }
}
