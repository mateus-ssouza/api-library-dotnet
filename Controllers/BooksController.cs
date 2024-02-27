using ApiBiblioteca.Domain.Models.Interfaces;
using ApiBiblioteca.Domain.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Book model)
        {
            await _bookRepository.Add(model);

            return StatusCode(201, "Book registered successfully!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Book model, Guid id)
        {
            await _bookRepository.Update(id, model);

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
