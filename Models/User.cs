using System;
using System.Collections.Generic;

namespace demo1111.Models;

public partial class User
{
    public long IdUser { get; set; }

    public string? Surname { get; set; }

    public string? Name { get; set; }

    public string? Lastname { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public long? IdRole { get; set; }

    public virtual Role? IdRoleNavigation { get; set; }

    public virtual ICollection<Zakaz> Zakazs { get; set; } = new List<Zakaz>();
}
