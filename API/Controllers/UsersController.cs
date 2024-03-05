﻿using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
public class UsersController : BaseApiController
{
    private readonly DataContext _db;
public UsersController(DataContext db)
{
    _db = db;
}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
             var user = await _db.AppUsers.ToListAsync();
            return user;
} 
            [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser (int id){
            return await _db.AppUsers.FirstOrDefaultAsync(u=> u.Id == id);
}
}
