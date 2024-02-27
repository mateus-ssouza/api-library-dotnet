using ApiBiblioteca.Domain.Models.Interfaces;
using ApiBiblioteca.Domain.Models;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Add([FromBody] Loan model)
        {
            await _loanRepository.Add(model);

            return StatusCode(201, "Loan registered successfully!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Loan model, Guid id)
        {
            await _loanRepository.Update(id, model);

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
