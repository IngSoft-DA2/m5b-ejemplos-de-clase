### Paso 1: Crear el nuevo proyecto

Es conveniente, antes de empezar, cerciorarnos que estamos trabajando con la versión de .NET correcta. Para eso podemos utilizar el siguiente comando:

```bash
dotnet --version
```

Debería retornar la versión instalada de .NET 8.

Ahora si, comenzamos:

**Creación del Proyecto**

1.	Abre una terminal (recomendación: ejecutar como administrador) y ubícate en el directorio donde deseas crear el proyecto.

2.	Utiliza el siguiente comando para crear una solución:

```bash
dotnet new sln -n shop
```

3.	Crea la Web API:

```bash
dotnet new webapi -n shop.WebApi --use-minimal-apis false --auth none
```

4.	Agrega la Web API a la solución:

```bash
dotnet sln add shop.WebApi
```

### Paso 2: Ejecutar el Proyecto

Dirígete al directorio donde se encuentra tu Web API y ejecuta el siguiente comando para iniciar el servidor:

```bash
cd shop.WebApi/
dotnet run
```


---

### Paso 3: Crear el Primer Controlador

Un controlador es una clase que maneja las solicitudes HTTP (como GET, POST, PUT, DELETE) y determina cómo responder a ellas. Su rol principal es actuar como intermediario entre el modelo de datos y la vista. En nuestro caso, el controlador heredará de `ControllerBase`, una clase de [ASP.NET](http://asp.net/) Core que nos proporcionará las funcionalidades básicas necesarias para crear el controlador.

### Creación del Controlador

1. Debes pararte en la careta Controllers.
2. Crea un nuevo archivo para el controlador:

```bash
cd Controllers/
dotnet new apicontroller -n ProductsController
```