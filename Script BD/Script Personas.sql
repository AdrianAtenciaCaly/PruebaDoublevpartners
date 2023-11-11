CREATE TABLE Personas (
    Identificador INT PRIMARY KEY   IDENTITY(1,1),
    Nombres VARCHAR(50),
    Apellidos VARCHAR(50),
    NumeroIdentificacion VARCHAR(20),
    Email VARCHAR(50),
    TipoIdentificacion VARCHAR(10),
    FechaCreacion DATETIME,
    NumeroIdentificacionConcat AS TipoIdentificacion + NumeroIdentificacion,
    NombresApellidosConcat AS Nombres + ' ' + Apellidos
);