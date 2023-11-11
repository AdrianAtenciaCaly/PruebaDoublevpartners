CREATE TABLE Usuarios (
    Identificador INT PRIMARY KEY  IDENTITY(1,1),
    Usuario VARCHAR(100),
    Pass VARCHAR(100),
    FechaCreacion DATETIME
);