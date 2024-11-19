using System;
using System.Collections.Generic;

namespace demo1111.Models;

public partial class Category
{
    public long IdCateg { get; set; }

    public string? Categ { get; set; }

    public virtual ICollection<Prod> Prods { get; set; } = new List<Prod>();
}
