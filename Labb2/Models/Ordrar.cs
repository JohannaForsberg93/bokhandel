using System;
using System.Collections.Generic;

namespace Labb2.Models;

public partial class Ordrar
{
    public int OrderId { get; set; }

    public string Isbn { get; set; } = null!;

    public int? Antal { get; set; }

    public virtual Böcker IsbnNavigation { get; set; } = null!;

    public virtual OrderDetail Order { get; set; } = null!;
}
