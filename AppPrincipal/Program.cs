using System.Reflection;
using System.Text.Json;
using Contratos;

namespace AppPrincipal;

internal class Program
{
    static void Main()
    {
       
        var empleado = new Empleado { Nombre = "Bruno Acosta", Puesto = "Desarrollador .NET" };
        
        var path = Path.Combine(AppContext.BaseDirectory, "../../../../PluginFormal/bin/Debug/net8.0/PluginFormal.dll");
        //var path = "/Users/bacosta/ORT2026S1/lab/reflection/demo-reflection1/PluginFormal/bin/Debug/net8.0/PresentacionInformal.dll";
        Console.WriteLine($"Cargando plugin desde:\n{path}\n");

        
        Assembly asm = Assembly.LoadFrom(path);
       
        var tipo = asm.GetTypes()
            .FirstOrDefault(t =>
                !t.IsInterface &&               
                !t.IsAbstract &&                
                t.GetMethod("Presentar") != null);

        if (tipo == null)
        {
            Console.WriteLine("No se encontró clase con método Presentar().");
            return;
        }

        object instancia = Activator.CreateInstance(tipo)!;
        MethodInfo metodo = tipo.GetMethod("Presentar")!;
        Console.WriteLine("Resultado de la presentación cargada por reflection:\n");
        metodo.Invoke(instancia, new object[] { JsonSerializer.Serialize(empleado) });
        
        
        
    }
}
 /*
  * Builear DLL
  * dotnet build PluginFormal   
  *
  * dotnet run --project AppPrincipal
  * 
  */