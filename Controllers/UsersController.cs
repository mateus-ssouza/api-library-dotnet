using Microsoft.AspNetCore.Mvc;
using ApiBiblioteca.Domain.Models.Interfaces;
using ApiBiblioteca.Application.ViewModel;
using AutoMapper;
using ApiBiblioteca.Domain.Models;
using ApiBiblioteca.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace ApiBiblioteca.Controllers
{
    [Authorize]
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;

        public UsersController(IUserRepository userRepository, IMapper mapper, TokenService tokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserViewModel viewModel)
        {
            var userId = Guid.Parse(_tokenService.GetIdByToken(HttpContext));

            var userExists = await _userRepository.GetById(userId);
            if (userExists == null) return NotFound("User not registered!");

            var isRegisteredEmail = await _userRepository.IsRegisteredEmail(viewModel.Email);
            if (isRegisteredEmail && userExists.Email != viewModel.Email) return BadRequest("This email is already in use.");

            var user = _mapper.Map<User>(viewModel);
            var passwordHash = CryptographyService.Encrypt(viewModel.Password);
            user.Password = passwordHash;

            await _userRepository.Update(userId, user);

            return Ok("User updated successfully!");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var userId = Guid.Parse(_tokenService.GetIdByToken(HttpContext));

            var userExists = await _userRepository.ExistsUser(userId);
            if (userExists == false) return NotFound("User not registered!");

            await _userRepository.Delete(userId);

            return Ok("User removed successfully!");
        }
    }
}
