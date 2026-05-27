import { HttpInterceptorFn } from '@angular/common/http';

// ─────────────────────────────────────────────────────────────────────────────
// HTTP INTERCEPTOR — AUTH
//
// Un interceptor es un "middleware" para requests HTTP.
// Intercepta TODOS los requests que salen de la app antes de que lleguen
// al servidor, y puede modificarlos (por ejemplo, agregar headers).
//
// ¿Por qué usamos esto?
//   Sin interceptor, tendríamos que agregar el header Authorization
//   manualmente en cada llamada HTTP de cada service. El interceptor
//   lo hace automáticamente una sola vez.
//
// En Angular 14+ podemos definir interceptores como funciones puras
// (HttpInterceptorFn) en lugar de clases. Es más simple y tree-shakeable.
// ─────────────────────────────────────────────────────────────────────────────
export const authInterceptor: HttpInterceptorFn = (req, next) => {

  // ── Paso 1: Leer el token del localStorage ─────────────────────────────────
  // Leemos directamente del localStorage. Si no hay sesión activa,
  // getItem devuelve null y dejamos pasar el request sin modificar.
  const token = localStorage.getItem('auth_token');

  // ── Paso 2: Si no hay token, dejamos pasar el request tal como está ─────────
  if (!token) {
    // `next(req)` envía el request original al servidor sin cambios.
    return next(req);
  }

  // ── Paso 3: Clonar el request y agregar el header Authorization ─────────────
  // Los objetos HttpRequest son inmutables — no podemos modificarlos directamente.
  // Debemos usar .clone() para crear una copia con las modificaciones que queremos.
  const authenticatedRequest = req.clone({
    setHeaders: {
      // El formato estándar es "Bearer <token>".
      // El backend valida este header para autorizar la operación.
      Authorization: `Bearer ${token}`,
    },
  });

  // ── Paso 4: Enviar el request modificado ───────────────────────────────────
  // Pasamos el request clonado (con el header) en lugar del original.
  return next(authenticatedRequest);
};
