import { CommonModule } from '@angular/common';
import { Component, inject,  } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.html',
  styleUrl: './home.scss'
})

export class Home {
  authService = inject(AuthService);
}