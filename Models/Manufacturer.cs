using System;
using System.Collections.Generic;

namespace demo1111.Models;

public partial class Manufacturer
{
    public long IdManuf { get; set; }

    public string? Manuf { get; set; }

    public virtual ICollection<Prod> Prods { get; set; } = new List<Prod>();
}
