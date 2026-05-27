import { Component } from '@angular/core';
import { RouterModule, Router } from '@angular/router';

import { AuthService } from '../../../core/services/auth.service';
import { UserRolePipe } from '../../pipes/user-role.pipe';

// ─────────────────────────────────────────────────────────────────────────────
// NAVBAR COMPONENT
//
// Muestra los enlaces de navegación y el estado de la sesión del usuario.
// Es el lugar natural para usar el UserRolePipe: queremos mostrar el rol
// del usuario logueado de forma legible en la barra de navegación.
// ─────────────────────────────────────────────────────────────────────────────
@Component({
  selector: 'app-navbar',
  // Importamos el pipe directamente en el componente (app standalone).
  // No necesitamos un módulo compartido — cada componente declara sus deps.
  imports: [RouterModule, UserRolePipe],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {

  constructor(
    public authService: AuthService,
    private router: Router
  ) {}

  // Delega el logout al AuthService y redirige al login.
  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
