import { Component, inject } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign',
  imports: [CommonModule, FormsModule, MatInputModule, MatIconModule, MatButtonModule],
  templateUrl: './sign.html',
  styleUrl: './sign.scss',
})
export class Sign {
  authService = inject(AuthService)
  router = inject(Router)
  model: any = {}
  sign() {
    this.authService
      .register(this.model)
      .subscribe({ 
        next: (r) => {
          console.log(r);
        }, 
        error: (e) => console.log(e.error) 
      });
  }
}

