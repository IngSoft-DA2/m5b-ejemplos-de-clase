import { Injectable } from '@angular/core';

// ─────────────────────────────────────────────────────────────────────────────
// AUTH SERVICE
//
// Responsabilidad: manejar el estado de autenticación del usuario.
//
// En esta demo el login es completamente mockeado — no hay API real.
// El objetivo es mostrar:
//   1. Cómo se guarda un token en localStorage
//   2. Cómo se lee y elimina ese token
//   3. Cómo el resto de la app (guard, interceptor) consume este service
// ─────────────────────────────────────────────────────────────────────────────
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  // Clave que usamos en localStorage para guardar el token.
  // Definirla como constante evita typos al leer/escribir.
  private readonly TOKEN_KEY = 'auth_token';

  // ──────────────────────────────────────────────────────────────────────────
  // LOGIN
  //
  // Simula la respuesta de un backend. En una app real, haríamos un
  // HTTP POST y el servidor nos devolvería el JWT.
  //
  // Aquí generamos un token falso con Math.random() solo para demostrar
  // que el token cambia en cada sesión, como lo haría un JWT real.
  // ──────────────────────────────────────────────────────────────────────────
  public login(email: string, password: string): boolean {
    const isValidCredentials = email === 'user' && password === '1234';

    if (!isValidCredentials) {
      alert('Credenciales inválidas. Usá user / 1234');
      return false;
    }

    // Generamos un token simulado. En producción este valor vendría del backend.
    const fakeToken = `mock-token-${Math.random().toString(36).substring(2)}`;

    // ── LocalStorage ─────────────────────────────────────────────────────────
    // Guardamos el token en localStorage para que persista entre recargas.
    // localStorage.setItem(clave, valor) siempre guarda strings.
    localStorage.setItem(this.TOKEN_KEY, fakeToken);

    return true;
  }

  // ──────────────────────────────────────────────────────────────────────────
  // LOGOUT
  //
  // Eliminamos el token del localStorage para "cerrar sesión".
  // Si el usuario recarga la página después de esto, ya no estará autenticado.
  // ──────────────────────────────────────────────────────────────────────────
  public logout(): void {
    // localStorage.removeItem(clave) elimina solo esa entrada.
    localStorage.removeItem(this.TOKEN_KEY);
  }

  // ──────────────────────────────────────────────────────────────────────────
  // VERIFICACIÓN DE SESIÓN
  //
  // Comprobamos si hay un token guardado. Si localStorage.getItem devuelve
  // null, no hay sesión activa. Esto funciona incluso tras recargar la página,
  // a diferencia de una variable en memoria que se resetearía.
  // ──────────────────────────────────────────────────────────────────────────
  public isUserLogged(): boolean {
    // localStorage.getItem devuelve el string guardado, o null si no existe.
    return localStorage.getItem(this.TOKEN_KEY) !== null;
  }

  // ──────────────────────────────────────────────────────────────────────────
  // OBTENER TOKEN
  //
  // Expone el token para que el interceptor pueda leerlo y adjuntarlo
  // en cada request HTTP. Devuelve null si no hay sesión activa.
  // ──────────────────────────────────────────────────────────────────────────
  public getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  // ──────────────────────────────────────────────────────────────────────────
  // ROL DEL USUARIO
  //
  // En una app real el rol vendría dentro del JWT (como un claim).
  // Aquí lo mockeamos para demostrar el pipe de transformación de roles.
  // ──────────────────────────────────────────────────────────────────────────
  public getUserRole(): string {
    return this.isUserLogged() ? 'user' : 'guest';
  }
}
