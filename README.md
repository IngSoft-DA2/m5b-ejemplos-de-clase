## Interceptors

Los **Interceptors** en Angular permiten interceptar y modificar requests y responses HTTP antes de que lleguen al servidor o al componente.

### Usos comunes
- Agregar tokens de autenticación (JWT)
- Manejar errores globalmente
- Loggear requests
- Mostrar loaders/spinners

Funcionan como una capa intermedia dentro del `HttpClient`.

### Ejemplo
```ts
export class AuthInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler) {
    const cloned = req.clone({
      setHeaders: {
        Authorization: `Bearer token`
      }
    });

    return next.handle(cloned);
  }
}
```

---

## Pipes

Los **Pipes** transforman datos directamente en el template HTML sin modificar el valor original.

### Usos comunes
- Formatear fechas
- Transformar texto
- Mostrar monedas
- Filtrar o transformar información visualmente

### Pipes built-in de Angular
- `date`
- `uppercase`
- `currency`
- `percent`

### Ejemplo
```html
<p>{{ nombre | uppercase }}</p>
<p>{{ fecha | date:'dd/MM/yyyy' }}</p>
```

### Pipe custom
```ts
@Pipe({ name: 'capitalize' })
export class CapitalizePipe implements PipeTransform {
  transform(value: string): string {
    return value.charAt(0).toUpperCase() + value.slice(1);
  }
}
```