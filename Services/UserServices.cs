using System.Security.Cryptography;
using System.Text;
using CapiWear_API.Data.Repositories;
using CapiWear_API.DTOs;
using CapiWear_API.Models;

namespace CapiWear_API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo) => _repo = repo;

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await _repo.GetAllAsync();
            return users.Select(MapToDTO);
        }

        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            return user is null ? null : MapToDTO(user);
        }

        public async Task<UserDTO> CreateAsync(UserCreateDTO dto)
        {
            var emailNorm = dto.Email.Trim().ToLowerInvariant();
            var existing = await _repo.GetByEmailAsync(emailNorm);
            if (existing is not null)
                throw new InvalidOperationException("Email já utilizado.");

            var now = DateTime.UtcNow;
            var user = new User
            {
                Name       = dto.Name.Trim(),
                Email      = emailNorm,
                Address    = dto.Address,
                PasswordHash = Hash(dto.Password),
                CreatedAt  = now,
                UpdatedAt  = now
            };

            var created = await _repo.AddAsync(user);
            return MapToDTO(created);
        }

        public async Task<UserDTO?> UpdateAsync(int id, UserUpdateDTO dto)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user is null) return null;

            var emailNorm = dto.Email.Trim().ToLowerInvariant();

            // checa conflito de email
            var sameEmail = await _repo.GetByEmailAsync(emailNorm);
            if (sameEmail is not null && sameEmail.Id != id)
                throw new InvalidOperationException("Email já está em uso por outro usuário.");

            user.Name = dto.Name.Trim();
            user.Email = emailNorm;
            user.Address = dto.Address;
            if (!string.IsNullOrWhiteSpace(dto.NewPassword))
                user.PasswordHash = Hash(dto.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;

            var updated = await _repo.UpdateAsync(user);
            return updated is null ? null : MapToDTO(updated);
        }

        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);

        private static UserDTO MapToDTO(User u) => new()
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            Address = u.Address,
            CreatedAt = u.CreatedAt,
            UpdatedAt = u.UpdatedAt
        };

        // Hash simples (troque por BCrypt/Argon2 em prod)
        private static string Hash(string value)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(value));
            return Convert.ToBase64String(bytes);
        }
    }
}
