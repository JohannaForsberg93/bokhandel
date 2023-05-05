using System;
using System.Collections.Generic;

namespace Labb2.Models;

public partial class Böcker
{
    public string Isbn13 { get; set; } = null!;

    public string? Titel { get; set; }

    public string? Språk { get; set; }

    public int? Pris { get; set; }

    public int? Sidor { get; set; }

    public DateTime? Utgivningsdatum { get; set; }

    public int? FörfattareId { get; set; }

    public virtual Författare? Författare { get; set; }

    public virtual ICollection<LagerSaldo> LagerSaldos { get; } = new List<LagerSaldo>();

    public virtual ICollection<Ordrar> Ordrars { get; } = new List<Ordrar>();
}
