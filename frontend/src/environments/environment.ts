// ─────────────────────────────────────────────────────────────────────────────
// VARIABLES DE ENTORNO — DESARROLLO
//
// Angular reemplaza automáticamente este archivo con environment.prod.ts
// cuando hacemos `ng build` con --configuration production.
//
// ¿Por qué usamos esto?
//   En lugar de hardcodear URLs y claves en cada service, las centralizamos
//   aquí. Así, cambiar de entorno (dev → prod) no requiere tocar los services.
// ─────────────────────────────────────────────────────────────────────────────
export const environment = {
  // Indica si la app corre en modo producción.
  // Angular usa esta flag internamente para activar optimizaciones.
  production: false,

  // URL base de la API de películas (The Movie Database).
  // En producción podría apuntar a otra URL o a nuestro propio backend.
  apiUrl: 'https://api.themoviedb.org/3',

  // API key de TMDB. En producción usaríamos una variable inyectada
  // por el servidor de CI/CD, nunca un valor hardcodeado en el repo.
  apiKey: '36eb10aa56ea823b1b3c7370a05aeb09',
};
