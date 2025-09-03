using Microsoft.AspNetCore.Mvc;
using CapiWear_API.DTOs;
using CapiWear_API.Services;

namespace CapiWear_API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _svc;
        public UserController(IUserService svc) => _svc = svc;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
            => Ok(await _svc.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            var user = await _svc.GetByIdAsync(id);
            return user is null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Create([FromBody] UserCreateDTO dto)
        {
            var created = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserDTO>> Update(int id, [FromBody] UserUpdateDTO dto)
        {
            var updated = await _svc.UpdateAsync(id, dto);
            return updated is null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _svc.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
