using System;
using System.Collections.Generic;
using System.Linq;

namespace demo1111.Models;

public partial class Zakaz
{
    public long IdZakaz { get; set; }

    public DateOnly? DateCr { get; set; }

    public DateOnly? DateDeliver { get; set; }

    public long? IdPunkt { get; set; }

    public long? IdClient { get; set; }

    public long? Code { get; set; }

    public long? IdStatus { get; set; }

    public virtual User? IdClientNavigation { get; set; }

    public virtual Punkt? IdPunktNavigation { get; set; }

    public virtual Status? IdStatusNavigation { get; set; }

    public virtual ICollection<ZakazProd> ZakazProds { get; set; } = new List<ZakazProd>();
    public float TotalCost => ZakazProds.Sum(x => x.IdProdNavigation.ItogCost).Value;
}
