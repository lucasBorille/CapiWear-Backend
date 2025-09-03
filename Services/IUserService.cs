using CapiWear_API.DTOs;

namespace CapiWear_API.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO?> GetByIdAsync(int id);
        Task<UserDTO> CreateAsync(UserCreateDTO dto);
        Task<UserDTO?> UpdateAsync(int id, UserUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
