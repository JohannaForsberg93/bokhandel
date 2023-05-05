using System;
using System.Collections.Generic;

namespace Labb2.Models;

public partial class TitlarPerFörfattare
{
    public string Namn { get; set; } = null!;

    public string Ålder { get; set; } = null!;

    public string Titlar { get; set; } = null!;

    public string Lagervärde { get; set; } = null!;
}
