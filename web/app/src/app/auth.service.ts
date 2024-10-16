import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly ADMIN_USERNAME = 'Admin';
  private readonly ADMIN_PASSWORD = '1234';
  constructor(private router: Router) {}

  login(uname: string, pword: string): boolean {
    if (uname === this.ADMIN_USERNAME && pword === this.ADMIN_PASSWORD) {
      localStorage.setItem('isLoggedIn', 'true');
      return true;
    } else {
      return false;
    }
  }

  isLoggedIn(): boolean {
    return localStorage.getItem('isLoggedIn') === 'true';
  }

  logout(): void {
    localStorage.removeItem('isLoggedIn');
    this.router.navigate(['/login']);
  }
}

