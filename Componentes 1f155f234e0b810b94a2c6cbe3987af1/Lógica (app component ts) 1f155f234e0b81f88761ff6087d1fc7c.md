# Lógica (app.component.ts)

## A nivel de implementación se ve así:

```tsx
// app.component.ts

import { Component } from '@angular/core';

@Component({
  selector: 'app-ejemplo',
  standalone: true,
  imports: [],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  // AQUÍ LÓGICA...
}
```

### Ahora… ¿qué es cada cosa?

![*Material Daniel Acevedo*](L%C3%B3gica%20(app%20component%20ts)%201f155f234e0b81f88761ff6087d1fc7c/image.png)

*Material Daniel Acevedo*

## ¿Cómo importo un componente?

*Angular* nos permite dos permite dos formas de hacerlo:

1. Usando *standalone*
    
    ```tsx
    @Component({
      standalone: true,
      selector: 'profile-photo',
    })
    export class ProfilePhoto { }
    
    @Component({
      standalone: true,
      imports: [ProfilePhoto],
      template: `<profile-photo />`
    })
    export class UserProfile { }
    ```
    

1. Usando *NgModules*
    - ***Angular* lo está dejando de lado** pero pueden leer como sería en el siguiente link: https://angular.dev/guide/ngmodules.

## Composición de componentes

*Ejemplo:*

![image.png](L%C3%B3gica%20(app%20component%20ts)%201f155f234e0b81f88761ff6087d1fc7c/image%201.png)

# Referencia componentes

 https://angular.dev/guide/components/importing