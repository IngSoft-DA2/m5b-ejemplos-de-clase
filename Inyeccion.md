#Dependency Injection
 
##Concepto de Inversión de Control (IoC):
**Definición**:
La Inversión de Control (IoC) es un principio de diseño en la programación que se refiere a la transferencia de la responsabilidad de la creación de objetos de una clase a un contenedor o marco. En lugar de que una clase instancie directamente sus dependencias, un contenedor externo se encarga de crear y proporcionar esas dependencias.
 
**Relación con la relación tradicional de dependencia**:
En un enfoque tradicional, una clase se crea junto con sus dependencias dentro de su propio constructor o método. Esto significa que la clase tiene conocimiento directo de sus dependencias, lo que lleva a un acoplamiento fuerte. Si necesitas cambiar una dependencia, debes modificar la clase, lo que dificulta el mantenimiento y la escalabilidad.
 
```csharp
public class Servicio {
    private Repositorio _repositorio = new Repositorio();
 
    public void Operacion() {
        _repositorio.HacerAlgo();
    }
}
 
```
 
**Ejemplo con IoC**:
 
```csharp
csharp
Copiar código
public class Servicio {
    private readonly Repositorio _repositorio;
 
    public Servicio(Repositorio repositorio) {
        _repositorio = repositorio;
    }
 
    public void Operacion() {
        _repositorio.HacerAlgo();
    }
}
 
```
 
Aquí, `Servicio` no crea su propia instancia de `Repositorio`, sino que recibe una ya creada. Esto permite cambiar la implementación de `Repositorio` sin modificar `Servicio`.
 
 
### **Relación con la Inyección de Dependencias (DI)**
 
La Inyección de Dependencias es una técnica que implementa el principio de Inversión de Control. A través de DI, se inyectan las dependencias en lugar de ser creadas por la clase misma.
 
**Tipos de DI**:
 
1. **Constructor Injection**: Las dependencias se pasan a través del constructor.
2. **Property Injection**: Las dependencias se establecen a través de propiedades públicas.
3. **Method Injection**: Las dependencias se pasan a métodos específicos cuando se invocan.
 
**Beneficios de IoC y DI**:
 
- **Desacoplamiento**: Las clases están menos acopladas entre sí, lo que facilita el cambio de implementaciones.
- **Testabilidad**: Permite utilizar mocks o stubs para pruebas unitarias.
- **Mantenibilidad**: Facilita la comprensión y el mantenimiento del código, ya que las dependencias son claras y están definidas en un solo lugar.
 
##Resumen
IoC es el principio que permite que un contenedor o marco asuma el control de la creación de objetos y sus dependencias. La Inyección de Dependencias es la técnica específica que utiliza IoC para proporcionar esas dependencias a las clases, promoviendo un diseño más limpio y mantenible.
 
- **Beneficios de la DI**:
    - Desacoplamiento del código.
    - Mejora en la testabilidad (mocking, pruebas unitarias).
    - Facilidad en el mantenimiento y la escalabilidad de aplicaciones.
- **Tipos de Inyección**:
    - Constructor Injection.
    - Property Injection.
    - Method Injection.
---
 
## **DI en .NET 8:**
 
Los servicios se registran en el contenedor durante la configuración de la aplicación, típicamente en el archivo Program.cs (o Startup.cs en versiones anteriores). Se utiliza la colección builder.Services para agregar servicios con métodos como:
 
- `AddTransient<TInterface, TImplementation>()`: Crea una nueva instancia cada vez que se solicita el servicio.
- `AddScoped<TInterface, TImplementation>()`: Crea una instancia por ámbito (por ejemplo, por solicitud HTTP en una API web).
- `AddSingleton<TInterface, TImplementation>()`: Crea una sola instancia compartida para toda la aplicación.
 
Esto permite desacoplar las clases, ya que no crean sus propias dependencias, sino que las reciben inyectadas.
 
Resolución de dependencias
El contenedor resuelve las dependencias automáticamente cuando se instancia una clase. Por ejemplo, en un controlador de API, puedes inyectar servicios a través del constructor:
 
var builder = WebApplication.CreateBuilder(args);
 
 
```csharp
// Registro de dependencias
builder.Services.AddTransient<IServicio, Servicio>();
builder.Services.AddScoped<IRepositorio, Repositorio>();
builder.Services.AddSingleton<ILogger, Logger>();
 
var app = builder.Build();
 
app.MapGet("/", (IServicio servicio) => servicio.Ejecutar());
 
app.Run();
```
 
Aquí, `IServicio` se resuelve como `Servicio` con una nueva instancia por solicitud (Transient), mientras que `IRepositorio` comparte instancia por solicitud (Scoped).
 
Diferencias en ciclos de vida
Transient: Ideal para servicios ligeros y sin estado, como operaciones rápidas. Cada inyección crea una instancia nueva.
Scoped: Útil en aplicaciones web para compartir estado dentro de una solicitud (ej. transacciones de base de datos).
Singleton: Para servicios costosos o con estado global (ej. configuración o cachés). Una sola instancia para toda la app, pero requiere cuidado con la concurrencia.
 
 
##Mas info en : https://learn.microsoft.com/es-es/dotnet/core/extensions/dependency-injection

---

## Métodos de extensión: qué son, por qué se usan, para qué sirven y cómo se usan

Un método de extensión es una técnica del lenguaje C# que permite “agregar” comportamientos a un tipo existente sin modificar su código ni recurrir a herencia. Se implementa como un método estático dentro de una clase estática, y se señala el tipo a extender mediante el primer parámetro precedido por la palabra clave this. A nivel de compilación es azúcar sintáctica: la llamada se traduce a una invocación estática convencional, pero desde el punto de vista de quién lo usa, se siente como un método propio del tipo.

Se usan porque mejoran la legibilidad y promueven la reutilización. Cuando tenemos configuraciones o convenciones que repetimos en múltiples proyectos (por ejemplo, la registración de dependencias) los métodos de extensión nos permiten encapsular esa lógica en un punto único y descubrirla con una API natural. También ayudan a desacoplar el código de arranque (Program.cs) de detalles de infraestructura, favoreciendo una composición clara de la aplicación.

En esta solución los usamos para centralizar la composición en un proyecto dedicado (ServiceFactory). En lugar de distribuir llamadas a registraciones de servicios por la Web API, exponemos un único método de extensión sobre IServiceCollection que concentra la selección de implementaciones y los ciclos de vida. De este modo, Program.cs queda reducido a una línea expresiva y la infraestructura queda encapsulada en un ensamblado específico.

Cómo se usan en la práctica. Primero se define el método de extensión en una clase estática accesible desde la Web API:

```csharp
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cine.ServiceFactory;

public static class DependencyInjection
{
    public static IServiceCollection AddCineDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}
```

Luego se invoca desde Program.cs importando el espacio de nombres del contenedor:

```csharp
using Cine.ServiceFactory;

builder.Services.AddCineDependencies(builder.Configuration);
```

¿Qué nos soluciona en este caso? Nos permite centralizar la composición (composition root) en ServiceFactory, mantener Program.cs limpio y ajeno a detalles de infraestructura, y facilitar el mantenimiento y las pruebas: cualquier cambio de implementación o ciclo de vida se realiza en un solo lugar sin tocar la capa Web.
