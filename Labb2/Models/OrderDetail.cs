using System;
using System.Collections.Generic;

namespace Labb2.Models;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public int ButiksId { get; set; }

    public DateTime? Datum { get; set; }

    public virtual Butiker Butiks { get; set; } = null!;

    public virtual ICollection<Ordrar> Ordrars { get; } = new List<Ordrar>();
}
