import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private isLogged = false;

  public login(email: string, password: string): boolean {
    if (email === 'user' && password === '1234') {
      this.isLogged = true;
      localStorage.setItem('token', 'token-back');
      return true;
    }

    alert('Invalid credentials');
    return false;
  }

  public logout(): void {
    this.isLogged = false;
    localStorage.removeItem('token');
  }

  public isUserLogged(): boolean {
    return this.isLogged;
  }
}
