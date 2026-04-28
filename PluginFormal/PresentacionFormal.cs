using System.Text.Json;
using Contratos;

namespace PluginFormal;

public class PresentacionFormal : IPresentacion
{
    public void Presentar(string json)
    {
        Empleado empleado = JsonSerializer.Deserialize<Empleado>(json)!;
        Console.WriteLine($"Buenos días, mi nombre es {empleado.Nombre} y me desempeño como {empleado.Puesto}.");
    }
}