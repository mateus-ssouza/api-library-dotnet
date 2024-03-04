using ApiBiblioteca.Application.ViewModel;
using ApiBiblioteca.Domain.DTOs;
using ApiBiblioteca.Domain.Models.Interfaces;
using ApiBiblioteca.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBiblioteca.Controllers
{
    [Route("api/v1/copies")]
    [ApiController]
    public class CopiesController : ControllerBase
    {
        private readonly ICopyRepository _copyRepository;
        private readonly IMapper _mapper;

        public CopiesController(ICopyRepository copyRepository, IMapper mapper)
        {
            _copyRepository = copyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var copies = await _copyRepository.GetAll();
            var copiesDTO = copies.Select(c => _mapper.Map<CopyDTO>(c));

            return Ok(copiesDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var copy = await _copyRepository.GetById(id);
            var copyDTO = _mapper.Map<CopyDTO>(copy);

            return copyDTO == null ? NotFound("Copy not found!") : Ok(copyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CopyViewModel viewModel)
        {
            var copy = _mapper.Map<Copy>(viewModel);
            copy.BookId = Guid.Parse("95865670-26C3-4641-E29B-08DC3C4E5E48");
            await _copyRepository.Add(copy);

            return StatusCode(201, "Copy registered successfully!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] CopyViewModel viewModel, Guid id)
        {
            var copy = _mapper.Map<Copy>(viewModel);
            await _copyRepository.Update(id, copy);

            return Ok("Copy updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _copyRepository.Delete(id);

            return Ok("Copy removed successfully!");
        }
    }
}
