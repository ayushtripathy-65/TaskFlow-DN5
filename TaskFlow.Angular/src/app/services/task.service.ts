import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface TaskItem {
  id: number;
  title: string;
  description: string;
  status: string;
  priority: string;
  dueDate?: Date;
  createdAt: Date;
  projectId: number;
  projectName: string;
}

export interface CreateTaskRequest {
  title: string;
  description: string;
  status: string;
  priority: string;
  dueDate?: Date;
  projectId: number;
}

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = 'http://localhost:5000/api/taskitems';

  constructor(private http: HttpClient) {}

  getTasks(): Observable<TaskItem[]> {
    return this.http.get<TaskItem[]>(this.apiUrl);
  }

  getTask(id: number): Observable<TaskItem> {
    return this.http.get<TaskItem>(this.apiUrl + '/' + id);
  }

  createTask(task: CreateTaskRequest): Observable<TaskItem> {
    return this.http.post<TaskItem>(this.apiUrl, task);
  }

  updateTask(id: number, task: CreateTaskRequest): Observable<void> {
    return this.http.put<void>(this.apiUrl + '/' + id, task);
  }

  deleteTask(id: number): Observable<void> {
    return this.http.delete<void>(this.apiUrl + '/' + id);
  }
}
