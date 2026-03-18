# Program.cs

Es el punto de entrada de nuestra aplicación, es donde vamos a configurar un host de tipo web application. También se configurará y se registrarán todos aquellos servicios requeridos para el funcionamiento de nuestra aplicación en conjunto con el proceso de middlewares y endpoints.

[← Atrás](README.md)

Es el punto de entrada de la aplicación. Se usa para configurar dos grandes cosas:

- El host de la aplicación.
- El *pipeline* de la aplicación.

## Host de la aplicación
```` csharp
var builder = WebApplication.CreateBuilder(args);
````
- Es el responsable de iniciar y gestionar la vida de la aplicación.
- Contiene la configuración de la aplicación y un servidor HTTP que escucha las solicitudes entrantes.

### 1. *(Opcional)* Ayuda a Swagger a generar la documentación de la API.
```` csharp
builder.Services.AddEndpointsApiExplorer();
````

### 2. *(Opcional)* Configura Swagger para generar la documentación de la API.

```` csharp
builder.Services.AddSwaggerGen();
````

### 3. Agrega los controladores a la aplicación.

```` csharp
builder.Services.AddControllers();
````

### 4. Construye e instancia la aplicación.

```` csharp
var app = builder.Build();
````

## Pipeline de la aplicación

### 5. *(Opcional y Condicional)* Configura Swagger para exponer la documentación de la API.

```` csharp
app.UseSwagger();
app.UseSwaggerUI();
````
- Se utiliza únicamente en entornos de desarrollo.

### 6. Habilita la redirección de HTTP a HTTPS.

```` csharp
app.UseHttpsRedirection();
````

### 7. Mapea los controladores a la aplicación.

```` csharp
app.MapControllers();
````

### 8. Inicia la aplicación y la mantiene en ejecución.

```` csharp
app.Run();
````