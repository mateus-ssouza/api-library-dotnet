using Microsoft.AspNetCore.Mvc;
using ApiBiblioteca.Domain.Models.Interfaces;
using ApiBiblioteca.Application.ViewModel;
using AutoMapper;
using ApiBiblioteca.Domain.DTOs;
using ApiBiblioteca.Domain.Models;

namespace ApiBiblioteca.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAll();
            var usersDTO = users.Select(u => _mapper.Map<UserDTO>(u));

            return Ok(usersDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userRepository.GetById(id);
            var userDTO = _mapper.Map<UserDTO>(user);

            return userDTO == null ? NotFound("User not found!") : Ok(userDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserViewModel viewModel)
        {
            var user = _mapper.Map<User>(viewModel);
            await _userRepository.Add(user);

            return StatusCode(201, "User registered successfully!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UserViewModel viewModel, Guid id)
        {
            var user = _mapper.Map<User>(viewModel);
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
