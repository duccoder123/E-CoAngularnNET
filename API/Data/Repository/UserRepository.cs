using API.DTOs;
using API.Entities;
using API.Interfaces.IRepository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MemberDto>> GetMemberAsync()
        {
          return await _context.AppUsers
                                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                                .ToListAsync();
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.AppUsers
            .Where(x => x.UserName.ToLower() == username.ToLower())
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
            
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await  _context.AppUsers.Include(p => p.Photos).FirstOrDefaultAsync(x => x.Id == id);
          
        }

        public async Task<AppUser> GetUserByNameAsync(string userName)
        {
            return await  _context.AppUsers.Include(p=>p.Photos).FirstOrDefaultAsync(x => x.UserName.ToLower() == userName.ToLower());
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
           return await _context.AppUsers
           .Include(p=>p.Photos)
           .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
            // tự động việc cập nhật đối tượng
        }
    }
}