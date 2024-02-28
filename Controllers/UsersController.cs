using Microsoft.AspNetCore.Mvc;
using ApiBiblioteca.Domain.Models.Interfaces;
using ApiBiblioteca.Application.Utils;
using ApiBiblioteca.Application.ViewModel;

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
        public async Task<IActionResult> Add([FromBody] UserViewModel viewModel)
        {
            var user = CreateUtil.UserCreate(viewModel);
            await _userRepository.Add(user);

            return StatusCode(201, "User registered successfully!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UserViewModel viewModel, Guid id)
        {
            var user = CreateUtil.UserCreate(viewModel);
            await _userRepository.Update(id, user);

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
