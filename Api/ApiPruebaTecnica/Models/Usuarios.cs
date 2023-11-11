using System;
using System.Collections.Generic;

namespace ApiPruebaTecnica.Models;

public partial class Usuarios
{
    public int? Identificador { get; set; }

    public string? Usuario { get; set; }

    public string? Pass { get; set; }

    public DateTime? FechaCreacion { get; set; }
}
