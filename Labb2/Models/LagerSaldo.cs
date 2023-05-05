using System;
using System.Collections.Generic;

namespace Labb2.Models;

public partial class LagerSaldo
{
    public int ButiksId { get; set; }

    public string Isbn { get; set; } = null!;

    public int? Antal { get; set; }

    public virtual Butiker Butiks { get; set; } = null!;

    public virtual Böcker IsbnNavigation { get; set; } = null!;
}
