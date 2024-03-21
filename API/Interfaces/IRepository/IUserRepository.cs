using API.DTOs;
using API.Entities;

namespace API.Interfaces.IRepository
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByNameAsync(string userName);
        Task<IEnumerable<MemberDto>> GetMemberAsync();
        Task <MemberDto> GetMemberAsync(string username);
    }
}