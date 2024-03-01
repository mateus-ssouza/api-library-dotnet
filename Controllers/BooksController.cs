﻿using ApiBiblioteca.Domain.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ApiBiblioteca.Application.ViewModel;
using AutoMapper;
using ApiBiblioteca.Domain.DTOs;
using ApiBiblioteca.Domain.Models;

namespace ApiBiblioteca.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookRepository.GetAll();
            var booksDTO = books.Select(b => _mapper.Map<BookDTO>(b));

            return Ok(booksDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var book = await _bookRepository.GetById(id);
            var bookDTO = _mapper.Map<BookDTO>(book);

            return bookDTO == null ? NotFound("Book not found!") : Ok(bookDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BookViewModel viewModel)
        {
            var book = _mapper.Map<Book>(viewModel);
            await _bookRepository.Add(book);

            return StatusCode(201, "Book registered successfully!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] BookViewModel viewModel, Guid id)
        {
            var book = _mapper.Map<Book>(viewModel);
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
