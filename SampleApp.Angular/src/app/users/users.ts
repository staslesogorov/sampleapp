import { Component, inject, OnInit, signal } from '@angular/core';
import { UsersService } from '../../services/users.service';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import User from '../../models/user.entity';

@Component({
  selector: 'app-users',
  imports: [CommonModule, MatTableModule],
  templateUrl: './users.html',
  styleUrl: './users.scss',
})
export class Users implements OnInit {
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