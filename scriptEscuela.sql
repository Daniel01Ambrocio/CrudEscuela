create database escuela;

use escuela

-- Crear tabla Divisiones
CREATE TABLE Divisiones (
    IDDivision INT IDENTITY(1,1) NOT NULL,
    NombreDivision NVARCHAR(150) NULL,
    DescripcionDiv NVARCHAR(255) NULL,
    PRIMARY KEY (IDDivision)
);
GO

-- Crear tabla Edificios
CREATE TABLE Edificios (
    IDEdificio INT IDENTITY(1,1) NOT NULL,
    NombreEdificio NVARCHAR(20) NULL,
    DescripcionEdif NVARCHAR(255) NULL,
    DivisionID INT NULL,
    PRIMARY KEY (IDEdificio),
    FOREIGN KEY (DivisionID) REFERENCES Divisiones (IDdivision)
);
GO

-- Crear tabla Especialidades
CREATE TABLE Especialidades (
    IDEspecialidad INT IDENTITY(1,1) NOT NULL,
    NombresEspecialidad NVARCHAR(255) NULL,
    DescripcionEsp NVARCHAR(255) NULL,
    DivisionID INT NULL,
    PRIMARY KEY (IDEspecialidad),
    FOREIGN KEY (DivisionID) REFERENCES Divisiones (IDDivision)
);
GO
select e.IDEspecialidad, e.NombresEspecialidad, e.DescripcionEsp, d.NombreDivision from Especialidades as e INNER JOIN 
Divisiones d ON e.DivisionID = d.IDDivision


-- Insertar datos en la tabla Divisiones
INSERT INTO Divisiones (NombreDivision, DescripcionDiv)
VALUES 
    ('Divisi�n de Ciencias', 'Encargada de todas las materias cient�ficas como biolog�a, f�sica, qu�mica, etc.'),
    ('Divisi�n de Letras', 'Encargada de las materias de humanidades, literatura, historia, etc.'),
    ('Divisi�n de Artes', 'Encargada de las materias relacionadas con las artes pl�sticas, m�sica, teatro, etc.'),
    ('Divisi�n de Tecnolog�a', 'Encargada de las materias relacionadas con programaci�n, rob�tica, electr�nica, etc.');
GO

-- Insertar datos en la tabla Edificios
INSERT INTO Edificios (NombreEdificio, DescripcionEdif, DivisionID)
VALUES 
    ('Edificio A', 'Edificio principal donde se imparten las clases de ciencias.', 1),
    ('Edificio B', 'Edificio dedicado a las clases de letras y humanidades.', 2),
    ('Edificio C', 'Edificio donde se realizan actividades relacionadas con las artes.', 3),
    ('Edificio D', 'Edificio que alberga los laboratorios de tecnolog�a y programaci�n.', 4);
GO

-- Insertar datos en la tabla Especialidades
INSERT INTO Especialidades (NombresEspecialidad, DescripcionEsp, DivisionID)
VALUES 
    ('Biolog�a', 'Estudio de los seres vivos, sus estructuras y funciones.', 1),
    ('F�sica', 'Estudio de la materia, energ�a y las leyes que rigen el universo.', 1),
    ('Qu�mica', 'Estudio de la composici�n, propiedades y reacciones de la materia.', 1),
    ('Literatura Universal', 'Estudio de obras literarias importantes a nivel mundial.', 2),
    ('Historia Contempor�nea', 'Estudio de los acontecimientos hist�ricos m�s recientes.', 2),
    ('Pintura', 'Estudio de las t�cnicas art�sticas para crear obras visuales.', 3),
    ('M�sica', 'Estudio de la teor�a musical y la ejecuci�n de instrumentos.', 3),
    ('Programaci�n', 'Ense�anza de los lenguajes y conceptos b�sicos de la programaci�n de software.', 4),
    ('Rob�tica', 'Estudio de la creaci�n y manejo de robots.', 4);
GO
select *from Edificios


CREATE TABLE Roles (
    IDRol INT PRIMARY KEY IDENTITY(1,1),
    NombreRol NVARCHAR(50) NOT NULL
);


CREATE TABLE Usuarios (
    IDUsuario INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(100) NOT NULL UNIQUE,
    Contrasena NVARCHAR(255) NOT NULL,  -- Se recomienda hashearla
    RolID INT NOT NULL,
    Status NVARCHAR(10) DEFAULT 'Alta', -- Alta o Baja
    FOREIGN KEY (RolID) REFERENCES Roles(IDRol)
);


INSERT INTO Roles (NombreRol) VALUES 
('Gestor de Usuarios'), 
('Gestor Acad�mico'); 


INSERT INTO Usuarios (Nombre, Correo, Contrasena, RolID)
VALUES ('Juan P�rez', 'juan@correo.com', '1234', 1); -- Gestor de usuarios

INSERT INTO Usuarios (Nombre, Correo, Contrasena, RolID)
VALUES ('Ana Torres', 'ana@correo.com', '1234', 2); -- Gestor acad�mico

select * from usuarios