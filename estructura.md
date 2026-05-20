# Estructura de carpetas

<aside>
*No existe una única estructura correcta. Suele variar según la organización y el tipo de proyecto. Acá tienen un ejemplo para usar como guía.*
</aside>

# Una opción (Angular standalone)

```markdown
src/
├── app/
│   ├── core/
│   │   ├── guards/
│   │   ├── interceptors/
│   │   └── services/
│   │
│   ├── shared/
│   │   ├── components/
│   │   ├── directives/
│   │   └── pipes/
│   ├── features/
│   │   ├── auth/
│   │   ├── movies/
│   │   └── actors/
│   │
│   ├── layouts/
│   │   ├── main-layout/
│   │   └── auth-layout/
│   │
│   ├── app.component.ts
│   ├── app.routes.ts
│   └── app.config.ts
│
├── assets/
├── styles.css
├── index.html
└── main.ts
```

## Responsabilidades

- `core`: contiene lógica transversal y de infraestructura para toda la app. Ejemplos: servicios globales, guardas, interceptores y manejo de autenticación/sesión.
- `shared`: contiene piezas reutilizables de UI (componentes, directivas y pipes) que no dependen de una feature específica.
- `features`: organiza el proyecto por dominio funcional (por ejemplo, `auth`, `movies`, `actors`). Cada feature agrupa su propia lógica, vistas y rutas.

    Dentro de cada feature, una opción habitual es:
    
    ```markdown
    ├── components/
    │   ├── login-form/
    │   │   ├── login-form.component.ts
    │   │   ├── login-form.component.html
    │   │   └── login-form.component.scss
    │   └── register-form/
    │       ├── register-form.component.ts
    │       └── ...
    ├── services/
    │   └── auth.service.ts
    ├── pages/
    │   ├── login.page.ts
    │   └── register.page.ts
    └── auth.routes.ts
    ```
    
- `layouts`: envuelven otros componentes y definen la distribución general de pantalla. Por ejemplo, un layout con navbar persistente y un área central que cambia por ruta.

## Nota

- Si usás `NgModules`, podés reemplazar `app.config.ts` y `app.routes.ts` por `app.module.ts` y `app-routing.module.ts`.
- Evitá duplicar `assets` dentro de `app/` y en `src/`: mantené una única carpeta `src/assets/`.