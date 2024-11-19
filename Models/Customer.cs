using System;
using System.Collections.Generic;

namespace demo1111.Models;

public partial class Customer
{
    public long IdCustomer { get; set; }

    public string? Customer1 { get; set; }

    public virtual ICollection<Prod> Prods { get; set; } = new List<Prod>();
}
