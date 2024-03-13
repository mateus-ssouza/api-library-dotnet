using ApiBiblioteca.Application.ViewModel;
using ApiBiblioteca.Domain.DTOs;
using ApiBiblioteca.Domain.Models.Interfaces;
using ApiBiblioteca.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiBiblioteca.Controllers
{
    [Authorize]
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

        [Authorize(Policy = "Admin")]
        [HttpPost("book/{id}")]
        public async Task<IActionResult> Add([FromBody] CopyViewModel viewModel, Guid id)
        {
            var isRegisteredCopyCode = await _copyRepository.IsRegisteredCopyCode(viewModel.CopyCode);
            if (isRegisteredCopyCode) return BadRequest("You already have a copy with this copycode registered.");

            var copy = _mapper.Map<Copy>(viewModel);
            copy.BookId = id;
            await _copyRepository.Add(copy);

            return StatusCode(201, "Copy registered successfully!");
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] CopyViewModel viewModel, Guid id)
        {
            var copyExists = await _copyRepository.GetById(id);
            if (copyExists == null) return NotFound("Copy not found!");

            var isRegisteredCopyCode = await _copyRepository.IsRegisteredCopyCode(viewModel.CopyCode);
            if (isRegisteredCopyCode && copyExists.CopyCode != viewModel.CopyCode)
            {
                return BadRequest("You already have a copy with this copycode registered.");
            }

            var copy = _mapper.Map<Copy>(viewModel);
            await _copyRepository.Update(id, copy);

            return Ok("Copy updated successfully!");
        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var copyExists = await _copyRepository.GetById(id);
            if (copyExists == null) return NotFound("Copy not found!");

            await _copyRepository.Delete(id);

            return Ok("Copy removed successfully!");
        }
    }
}
