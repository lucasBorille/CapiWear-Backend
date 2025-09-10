using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapiWear_API.Data.Repositories;
using CapiWear_API.DTOs;
using CapiWear_API.Helpers;
using CapiWear_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CapiWear_API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _repo;
        public OrdersController(IOrderRepository repo) => _repo = repo;

        // GET /api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderReadDTO>>> List()
        {
            var orders = await _repo.GetAllOrdersAsync();
            return Ok(orders.Select(o => o.ToReadDTO()));
        }

        // GET /api/orders/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderReadDTO>> Get(int id)
        {
            var order = await _repo.GetOrderByIdAsync(id);
            if (order is null) return NotFound();
            return Ok(order.ToReadDTO());
        }

        // POST /api/orders
        [HttpPost]
        public async Task<ActionResult<OrderReadDTO>> Create([FromBody] OrderCreateDTO dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var order = new Order
            {
                UserId = dto.UserId,
                Subtotal = dto.Subtotal,
                Freight = dto.Freight
            };

            var created = await _repo.AddOrderAsync(order);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created.ToReadDTO());
        }

        // PATCH /api/orders/{id}
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<OrderReadDTO>> Update(int id, [FromBody] OrderUpdateDTO dto)
        {
            var existing = await _repo.GetOrderByIdAsync(id);
            if (existing is null) return NotFound();

            existing.ApplyUpdate(dto);

            var updated = await _repo.UpdateOrderAsync(existing);
            return Ok(updated!.ToReadDTO());
        }

        // DELETE /api/orders/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _repo.DeleteOrderAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
