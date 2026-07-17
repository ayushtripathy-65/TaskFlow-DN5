import { Routes } from '@angular/router';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', loadComponent: () => import('./components/login/login.component').then(m => m.LoginComponent) },
  { path: 'register', loadComponent: () => import('./components/register/register.component').then(m => m.RegisterComponent) },
  { 
    path: 'tasks', 
    canActivate: [authGuard],
    loadComponent: () => import('./components/task-list/task-list.component').then(m => m.TaskListComponent)
  },
  { 
    path: 'tasks/create', 
    canActivate: [authGuard],
    loadComponent: () => import('./components/task-create/task-create.component').then(m => m.TaskCreateComponent)
  },
  { 
    path: 'tasks/edit/:id', 
    canActivate: [authGuard],
    loadComponent: () => import('./components/task-edit/task-edit.component').then(m => m.TaskEditComponent)
  }
];
