using System;
using System.Collections.Generic;

namespace ApiPruebaTecnica.Models;

public partial class Personas
{
    public int? Identificador { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public string? NumeroIdentificacion { get; set; }

    public string? Email { get; set; }

    public string? TipoIdentificacion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? NumeroIdentificacionConcat { get; set; }

    public string? NombresApellidosConcat { get; set; }
}
