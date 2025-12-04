import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth',
  imports: [FormsModule, FormsModule, MatInputModule, MatFormFieldModule, MatIconModule, MatButtonModule],
  templateUrl: './auth.html',
  styleUrl: './auth.scss',
})
export class Auth {
  model:any = {}
  authService = inject(AuthService)
  router = inject(Router)
  login() {
    this.authService.login(this.model).subscribe({
      next: (v) => {console.log(v); this.router.navigate(["/"]) },
      error: (e) => console.error(e),
      complete: () => console.info('complete'),
    });
  }
}
