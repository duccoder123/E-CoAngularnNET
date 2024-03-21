using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[Authorize]
public class UsersController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
public UsersController(IUserRepository userRepository, IMapper mapper)
{
    _userRepository = userRepository;
    _mapper = mapper;
}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers(){
         var users = await _userRepository.GetMemberAsync();
        return Ok(users);
} 
    [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUserById (int id){
            var user = await  _userRepository.GetUserByIdAsync(id);
            var userToReturn = _mapper.Map<MemberDto>(user);
            return Ok(userToReturn);
            }
        public async Task<ActionResult<MemberDto>> GetUserByName(string userName){
            return await _userRepository.GetMemberAsync(userName);
        }
}
