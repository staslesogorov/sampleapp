import { Routes } from '@angular/router';
import { Users } from './users/users';
import { Home } from './home/home';
import { Auth } from './auth/auth';
import { Sign } from './sign/sign';

export const routes: Routes = [
    { path: 'users', component: Users },
    { path: 'home', component: Home },
    { path: '', component: Home },
    { path: 'auth', component: Auth },
    { path: 'sign', component: Sign }
];