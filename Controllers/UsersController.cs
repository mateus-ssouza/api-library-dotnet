using Microsoft.AspNetCore.Mvc;
using ApiBiblioteca.Domain.Models;
using ApiBiblioteca.Domain.Models.Interfaces;

namespace ApiBiblioteca.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] User model)
        {
            await _userRepository.Add(model);

            return StatusCode(201, "User registered successfully!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] User model, Guid id)
        {
            await _userRepository.Update(id, model);

            return Ok("User updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userRepository.Delete(id);

            return Ok("User removed successfully!");
        }
    }
}
