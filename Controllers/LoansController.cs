using ApiBiblioteca.Domain.Models.Interfaces;
using ApiBiblioteca.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using ApiBiblioteca.Application.Utils;
using ApiBiblioteca.Application.ViewModel;

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

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] LoanViewModel viewModel)
        {
            var loan = CreateUtil.LoanCreate(viewModel);
            loan.UserId = Guid.Parse("C48049BF-655F-4BE0-FDCF-08DC37D4B025");
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
