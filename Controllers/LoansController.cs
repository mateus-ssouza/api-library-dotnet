using ApiBiblioteca.Domain.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ApiBiblioteca.Application.Utils;
using ApiBiblioteca.Application.ViewModel;
using ApiBiblioteca.Infra.Repositories;
using ApiBiblioteca.Domain.Models;

namespace ApiBiblioteca.Controllers
{
    [Route("api/v1/loans")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanRepository _loanRepository;

        public LoansController(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var loans = await _loanRepository.GetAll();

            return Ok(loans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var loan = await _loanRepository.GetById(id);

            return loan == null ? NotFound("Book not found!") : Ok(loan);
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
