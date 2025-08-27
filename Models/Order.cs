using System;
using System.Collections.Generic;

namespace CapiWear_API.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Freight { get; set; }

    public decimal Total { get; set; }

    public DateTime PlacedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User? User { get; set; }
}
