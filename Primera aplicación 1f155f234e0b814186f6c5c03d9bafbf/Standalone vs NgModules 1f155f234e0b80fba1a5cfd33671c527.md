# Standalone vs. NgModules

### Son los enfoque que “coexisten” para manejar los componentes.

# Diferencias

- En pocas palabras, si trabajamos con el enfoque *NgModules* tenemos un unidades (NgModule) con responsabilidades.
- Con *standalone* se manejan los componentes de forma autónoma, no necesitamos ningún módulo adicional.
- Con *standalone* las dependencias se importan directamente dentro del componente. Con *NgModules* se hacen dentro del *NgModule.*

## Ejemplo de Standalone

```tsx
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-my-standalone',
  standalone: true, // ACÁ PUEDO INDICAR EL ENFOQUE (en parte)
  imports: [CommonModule],
  template: `<h1>Standalone Component!</h1>`,
})
export class MyStandaloneComponent {}
```

## Ejemplo de NgModule

- Se suele tener global y uno por funcionalidad.

```tsx
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyComponent } from './my.component';

@NgModule({
  declarations: [MyComponent], // Acá hago la referencia a mi componente
  imports: [CommonModule],
  exports: [MyComponent],
  providers: []
})
export class MyModule {}
```

### Responsabilidades que tienen…

1. Importar otros módulos
2. Exportar elementos. Por ejemplo, componentes declarados en el módulo
3. Proveer servicios

<aside>
📌

La recomendación es siempre usar *standalone* ya que es lo que Google esta promoviendo. Tengan cuidado con ChatGPT (aclaren en el *prompt* el enfoque que quieren…)

</aside>

*Más sobre NgModules:* https://angular.dev/guide/ngmodules/overview