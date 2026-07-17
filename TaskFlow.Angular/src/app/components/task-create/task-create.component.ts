import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { TaskService, CreateTaskRequest } from '../../services/task.service';

@Component({
  selector: 'app-task-create',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
    MatSelectModule
  ],
  templateUrl: './task-create.component.html',
  styleUrls: ['./task-create.component.css']
})
export class TaskCreateComponent {
  task: CreateTaskRequest = {
    title: '',
    description: '',
    status: 'Pending',
    priority: 'Medium',
    projectId: 1
  };
  error: string = '';

  constructor(private taskService: TaskService, private router: Router) {}

  onSubmit(): void {
    this.taskService.createTask(this.task).subscribe({
      next: () => {
        this.router.navigate(['/tasks']);
      },
      error: (err) => {
        this.error = err.error?.message || 'Failed to create task';
      }
    });
  }
}
