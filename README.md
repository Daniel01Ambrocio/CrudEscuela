# CrudEscuela
------------Especificaciones del desarrollo------------

Creado con:
-ASP.Net WebForm
-Bootstrap online
-SQL Server (Con la base de datos almacenada de forma local)
-Api de noticias: NewsAPI


------------FUNCIONAMIENTO------------
-Login
*Evalua si el usuario existe y tiene un estado de: Alta, en caso de ser así, redirige a la pagina correspondiente segun el rol.

-Rol de Gestor de usuarios
*Crud de usuarios con roles : Gestor de usuarios, Gestor academico y estudiantes
  -Dar de alta usuario
  *El correo debe ser unico y no debe estar asociado a ninguna otra cuenta existente


-Rol de Gestor Academico
*Crud divisiones
  -Eliminar division
  *No permite eliminar si la division está referenciada en la tabla de edificios o especialidades
*Crud edificios (Con llave foranea a divisiones)
*Crud especialidades(Con llave foranea a divisiones)


-Rol de Estudiante
*Consumo de Api de noticias con 3 opciones de tipo: Programación, NASA y Academico
*Buscar las noticias por fecha y mantener la fecha guardada y cargada en el calendar, sin importar que el usuario cambie de pagina.


-Cualquier usuario:
*Puede cambiar su contraseña y pueden volver a su pagina correspondiente
  -Contraseña segura
  *La contraseña debe contener. 6 caracteres como minimo, una letra mayuscula, una letra minuscula y un numero.
