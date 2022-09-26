import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
})
export class TodoComponent {
  public todos: Todo[] = [];
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private router:Router) {
    http
      .get<Todo[]>('https://localhost:7115/' + 'api/todos/gettodos')
      .subscribe(
        (result) => {
          this.todos = result;
        },
        (error) => console.error(error)
      );
  }

  delete = (id: string) => {
    this.http.delete<boolean>('https://localhost:7115/' + 'api/todos/deletetodo/' + id)
      .subscribe(
        (result) => console.log(result),
        (error) => console.log(error)
      )
  }

  edit(id:string) {
    this.router.navigate(['/edit/', id]);
    console.log(id);
  }
  
}

interface Todo {
  id: string;
  title: string;
  description: string;
  isDone: boolean;
  time: Date;
}
