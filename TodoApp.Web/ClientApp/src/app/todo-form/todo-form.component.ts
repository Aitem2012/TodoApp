import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: "app-todo-form",
  templateUrl: "./todo-form.component.html"
})


export class TodoFormComponent implements OnInit {
  public todo!: Todo;
  public myForm: NgForm | undefined;
  id: string | undefined;
  constructor(private http:HttpClient, private router: ActivatedRoute) {
    
  }
  onSubmit(form: NgForm) {
    console.log("Your form data:", form.value);
    this.todo = form.value;
    console.log(this.todo);
    this.http.post('https://localhost:7115/' + 'api/todos/createtodo', this.todo)
      .subscribe(
        (response) => console.log(response),
        (error) => console.log(error)
    );
    form.reset();
  }

  ngOnInit() {
    this.router.params.subscribe(params => this.id = params.id);
    console.log(this.id);
    this.http.get<Todo>('https://localhost:7115/' + 'api/todos/gettodobyid/' + this.id)
      .subscribe((data) => {
        this.todo = data;
        console.log(this.todo);
      }, (error) => {
        console.log(error)
      })

  }

  
}

interface Todo {
  id: string;
  title: string;
  description: string;
  isDone: boolean;
  time: Date;
}
