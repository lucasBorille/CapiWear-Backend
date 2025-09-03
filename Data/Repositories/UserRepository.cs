using Microsoft.EntityFrameworkCore;
using CapiWear_API.Models;

namespace CapiWear_API.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiDbContext _ctx;
        public UserRepository(ApiDbContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<User>> GetAllAsync() =>
            await _ctx.Users.AsNoTracking().ToListAsync();

        public async Task<User?> GetByIdAsync(int id) =>
            await _ctx.Users.FindAsync(id);

        public async Task<User?> GetByEmailAsync(string email) =>
            await _ctx.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User> AddAsync(User user)
        {
            _ctx.Users.Add(user);
            await _ctx.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateAsync(User user)
        {
            if (!await _ctx.Users.AnyAsync(u => u.Id == user.Id))
                return null;

            _ctx.Entry(user).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var u = await _ctx.Users.FindAsync(id);
            if (u is null) return false;
            _ctx.Users.Remove(u);
            await _ctx.SaveChangesAsync();
            return true;
        }
    }
}
