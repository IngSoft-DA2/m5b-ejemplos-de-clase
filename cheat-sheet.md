# Cheat sheet (Lista de comandos desde la consola)

Comando | Resultado
------------ | -------------
`dotnet new sln` | Creamos solución (principalmente útil para VisualStudio, cuando queremos abrir la solución y levantar los proyectos asociados)
`dotnet new webapi -n "Nombre del Proyecto"` | Crear un nuevo Proyecto del template WebApi
`dotnet sln add` | Asociamos el proyecto creado al .sln
`dotnet sln list` | Vemos todos los proyectos asociados a la solución
`dotnet new classlib -n "Nombre del Proyecto"` | Crear un nueva librería (standard)
`dotnet add "Nombre del Proyecto 1".csproj reference "Nombre del Proyecto 2".csproj` | Agrega una referencia al Proyecto 1 del Proyecto 2
`dotnet add package "Nombre del Package"` | Instala el Package al proyecto actual. Similar a cuando se agregaban paquetes de Nuget en .NET Framework.
`dotnet build` | Compilar y generar los archivos prontos para ser desplegados (_production build_)
`dotnet run` | Compilar y correr el proyecto
`dotnet ef migrations add "Nombre de la migración"` | Compilar y crear la migración para impactar en la base de datos
`dotnet ef database update` | Ejecutar las migraciones creadas

Siempre que se necesite ayuda con el comando o no se sabe cual usar, la mejor ayuda es correr `dotnet [COMANDO] -h` para una explicación de los parámetros del comando o cual es su funcionalidad. También se puede correr `dotnet -h` para poder ver los comandos disponibles

### Es necesario crear una solución? (SLN)

No, no es necesario. Sin embargo, tener una trae varias utilidades:
* Si usamos Visual Studio 2022, es necesario crearla para levantar todos los proyectos dentro del IDE
* Aunque no usemos VS2022, tener una solucion permite crear/compilar/manejar todos los proyectos involucrados juntos, sin tener que correr cada uno, por ejemplo. Se maneja todo como una única unidad.