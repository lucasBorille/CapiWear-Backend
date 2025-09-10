using System;
using System.ComponentModel.DataAnnotations;

namespace CapiWear_API.DTOs
{
    public record OrderCreateDTO(
        [Required] int UserId,
        [Range(0, double.MaxValue)] decimal Subtotal,
        [Range(0, double.MaxValue)] decimal Freight,
        string? Notes
    );
    public record OrderUpdateDTO(
        decimal? Subtotal,
        decimal? Freight
    );

    public record OrderReadDTO(
        int Id,
        int UserId,
        DateTime PlacedAt,
        DateTime UpdatedAt,
        decimal Subtotal,
        decimal Freight,
        decimal Total
    );
}
