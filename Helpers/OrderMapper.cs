using System;
using CapiWear_API.DTOs;
using CapiWear_API.Models;

namespace CapiWear_API.Helpers
{
    public static class OrderMapper
    {
        public static OrderReadDTO ToReadDTO(this Order o) =>
            new(
                o.Id,
                o.UserId,
                o.PlacedAt,
                o.UpdatedAt,
                o.Subtotal,
                o.Freight,
                o.Total
            );

        public static void ApplyUpdate(this Order o, OrderUpdateDTO dto)
        {
            if (dto.Subtotal.HasValue) o.Subtotal = dto.Subtotal.Value;
            if (dto.Freight.HasValue)  o.Freight  = dto.Freight.Value;

            o.Total = o.Subtotal + o.Freight;
            o.UpdatedAt = DateTime.UtcNow;
        }
    }
}
