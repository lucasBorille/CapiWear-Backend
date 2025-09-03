namespace CapiWear_API.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class UserCreateDTO
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Address { get; set; }
    }

    public class UserUpdateDTO
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Address { get; set; }
        public string? NewPassword { get; set; }
    }
}
