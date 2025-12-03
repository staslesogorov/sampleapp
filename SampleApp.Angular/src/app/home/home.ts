import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import User from '../../models/user.entity';
import { UsersService } from '../../services/users.service';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, MatTableModule],
  templateUrl: './home.html',
  styleUrl: './home.scss'
})


export class Home implements OnInit {

  users = signal<User[]>([])
  usersService = inject(UsersService)
  displayedColumns: string[] = ['id', 'name'];
  
  ngOnInit() {
    this.usersService.getAll().subscribe({
      next: (v) => this.users.set(v),
      error: (e) => console.error(e),
      complete: () => console.info('complete') 
  })
  }
}