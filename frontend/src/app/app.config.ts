import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';

import { routes } from './app.routes';
import { authInterceptor } from './core/interceptors/auth.interceptor';

// ─────────────────────────────────────────────────────────────────────────────
// APP CONFIG — punto central de configuración de la aplicación
//
// En una app standalone (sin NgModule) toda la configuración de providers
// se centraliza aquí. Es el equivalente al array `providers` del AppModule.
// ─────────────────────────────────────────────────────────────────────────────
export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),

    provideRouter(routes),

    // ── Registro del interceptor ───────────────────────────────────────────
    // withInterceptors([]) recibe un array de funciones interceptoras.
    // Angular los ejecuta en orden para cada request HTTP saliente.
    // Al agregar authInterceptor aquí, se aplicará automáticamente
    // a TODOS los requests que haga la app — sin tocar los services.
    provideHttpClient(
      withInterceptors([authInterceptor])
    ),
  ]
};
