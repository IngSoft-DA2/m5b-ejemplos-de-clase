// ─────────────────────────────────────────────────────────────────────────────
// VARIABLES DE ENTORNO — PRODUCCIÓN
//
// Este archivo reemplaza a environment.ts durante el build de producción.
// La sustitución la configura angular.json en la sección "fileReplacements".
//
// En una app real, apiKey vendría de una variable de entorno del servidor
// (process.env.API_KEY) o de un secrets manager, nunca commiteada aquí.
// ─────────────────────────────────────────────────────────────────────────────
export const environment = {
  production: true,

  // Misma URL base — en un proyecto real podría ser diferente.
  apiUrl: 'https://api.themoviedb.org/3',

  // En producción esta clave idealmente viene de un pipeline de CI/CD.
  apiKey: '36eb10aa56ea823b1b3c7370a05aeb09',
};
