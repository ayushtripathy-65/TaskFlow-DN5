import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { TaskService, CreateTaskRequest } from '../../services/task.service';

@Component({
  selector: 'app-task-edit',
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
  templateUrl: './task-edit.component.html',
  styleUrls: ['./task-edit.component.css']
})
export class TaskEditComponent implements OnInit {
  task: CreateTaskRequest = {
    title: '',
    description: '',
    status: 'Pending',
    priority: 'Medium',
    projectId: 1
  };
  taskId: number = 0;
  error: string = '';

  constructor(
    private taskService: TaskService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.taskId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadTask();
  }

  loadTask(): void {
    this.taskService.getTask(this.taskId).subscribe({
      next: (data) => {
        this.task = {
          title: data.title,
          description: data.description,
          status: data.status,
          priority: data.priority,
          dueDate: data.dueDate,
          projectId: data.projectId
        };
      },
      error: (err) => {
        this.error = 'Failed to load task';
      }
    });
  }

  onSubmit(): void {
    this.taskService.updateTask(this.taskId, this.task).subscribe({
      next: () => {
        this.router.navigate(['/tasks']);
      },
      error: (err) => {
        this.error = err.error?.message || 'Failed to update task';
      }
    });
  }
}
