import { Pipe, PipeTransform } from '@angular/core';

// ─────────────────────────────────────────────────────────────────────────────
// PIPE: UserRole
//
// Un pipe transforma un valor en el template antes de mostrarlo.
// Sintaxis en HTML: {{ valor | nombreDelPipe }}
//
// ¿Por qué usamos un pipe aquí?
//   El backend (o el JWT) nos devuelve roles en formato técnico: 'admin', 'user',
//   'guest'. El pipe los convierte a etiquetas legibles para el usuario final,
//   sin contaminar el componente con lógica de presentación.
//
// Además agrega un ícono visual para que la clase sea más ilustrativa.
//
// standalone: true → permite importarlo directamente en un componente
//             sin necesidad de declararlo en un NgModule.
// ─────────────────────────────────────────────────────────────────────────────
@Pipe({
  name: 'userRole',
  standalone: true,
})
export class UserRolePipe implements PipeTransform {

  // ── El método transform es el corazón del pipe ─────────────────────────────
  // Recibe el valor original (value) y devuelve el valor transformado.
  // Puede recibir argumentos adicionales: {{ value | userRole:'arg' }}
  transform(role: string): string {

    // Mapeamos cada rol técnico a su etiqueta legible con ícono.
    // Record<string, string> es un objeto tipado donde ambas claves y
    // valores son strings. Es más seguro que un objeto sin tipar.
    const roleLabels: Record<string, string> = {
      admin: '👑 Administrador',
      user:  '🎬 Usuario',
      guest: '👤 Invitado',
    };

    // Si el rol existe en el mapa, devolvemos su etiqueta.
    // Si no existe (rol desconocido), devolvemos un valor de fallback.
    return roleLabels[role] ?? '❓ Rol desconocido';
  }
}
