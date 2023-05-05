using System;
using System.Collections.Generic;

namespace Labb2.Models;

public partial class Butiker
{
    public int ButiksId { get; set; }

    public string? Butiksnamn { get; set; }

    public string? Adress { get; set; }

    public virtual ICollection<LagerSaldo> LagerSaldos { get; } = new List<LagerSaldo>();

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
}
