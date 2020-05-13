using Exchange.Web.DataAccess.Entities;
using Exchange.Web.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Web.DataAccess.Repositories
{
    public class UserRepository : IUserRepository<UserEntity>
    {
        private readonly UserManager<UserEntity> _userManager;

        public UserRepository(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task AddToRoleAsync(UserEntity entity, string role)
        {
            await _userManager.AddToRoleAsync(entity, role);
        }

        public async Task<IdentityResult> CreateAsync(UserEntity entity, string password)
        {
            return await _userManager.CreateAsync(entity, password);
        }

        public async Task<UserEntity> CreateAsync(UserEntity entity)
        {
            await _userManager.CreateAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(UserEntity entity)
        {
            await _userManager.DeleteAsync(entity);
        }

        public async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<UserEntity> GetOneByEmailAsync(string mail)
        {
            return await _userManager.FindByEmailAsync(mail);
        }

        public async Task<UserEntity> GetOneByIdAsync(long id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<UserEntity> GetOneByPhoneNumberAsync(string phoneNumber)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.Phone == phoneNumber);
        }

        public async Task<UserEntity> UpdateAsync(UserEntity entity)
        {
            await _userManager.UpdateAsync(entity);
            return entity;
        }
    }
}
