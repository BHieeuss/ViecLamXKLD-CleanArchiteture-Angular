import { Component } from '@angular/core';
import { FooterComponent } from '../../layout/footer/footer.component';
import { AuthService } from '../../auth.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from '../../layout/header/header.component';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FooterComponent, HeaderComponent, FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  username = '';
  password = '';
  loginError = '';

  constructor(private auth: AuthService, private router: Router){}
  ngOnInit(){}

  onSubmit(): void {
    if (this.auth.login(this.username, this.password)) {
      this.router.navigate(['/admin']);
    } else {
      this.loginError = 'Invalid username or password';
    }
  }
}