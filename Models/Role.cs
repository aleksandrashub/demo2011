using System;
using System.Collections.Generic;

namespace demo1111.Models;

public partial class Role
{
    public long IdRole { get; set; }

    public string? Role1 { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
