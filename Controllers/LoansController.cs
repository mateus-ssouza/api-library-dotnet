using ApiBiblioteca.Domain.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ApiBiblioteca.Application.Utils;
using ApiBiblioteca.Application.ViewModel;
using AutoMapper;
using ApiBiblioteca.Domain.DTOs;
using ApiBiblioteca.Domain.Models;

namespace ApiBiblioteca.Controllers
{
    [Route("api/v1/loans")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public LoansController(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var loans = await _loanRepository.GetAll();
            var loansDTO = loans.Select(l => _mapper.Map<LoanDTO>(l));

            return Ok(loansDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var loan = await _loanRepository.GetById(id);
            var loanDTO = _mapper.Map<LoanDTO>(loan);

            return loanDTO == null ? NotFound("Book not found!") : Ok(loanDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] LoanViewModel viewModel)
        {
            var loan = CreateUtil.LoanCreate(viewModel);
            loan.UserId = Guid.Parse("6D271B17-7628-4B5B-6609-08DC391F9DCF");
            await _loanRepository.Add(loan);

            return StatusCode(201, "Loan registered successfully!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] LoanViewModel viewModel, Guid id)
        {
            var loan = CreateUtil.LoanCreate(viewModel);
            await _loanRepository.Update(id, loan);

            return Ok("Loan updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _loanRepository.Delete(id);

            return Ok("Loan removed successfully!");
        }
    }
}
