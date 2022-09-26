import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';

@Component({
  selector: "app-todo-form",
  templateUrl: "./todo-form.component.html"
})


export class TodoFormComponent implements OnInit {
  todo: Todo = {
    id: "",
    isDone: false,
    time: new Date(),
    title: "",
    description : ""
  };
  _id: string | undefined;
  title: string | any;
  description: string | any;
  time: Date | undefined;
  isDone: boolean | any;
  myForm: NgForm | undefined;
  myFormGroup: FormGroup | undefined;
  constructor(private http:HttpClient, private router: ActivatedRoute, private route: Router) {
    
  }
  onSubmit(myForm: NgForm) {
    console.log("Your form data:", myForm.value);
    this.todo = myForm.value;
    if (this.todo.id) {
      console.log(this.todo);
      this.update(this.todo);
    } else {
      if (!this.todo.isDone) {
        this.todo.isDone = false;
      }
      console.log(this.todo);
      this.http.post('https://localhost:7115/' + 'api/todos/createtodo', this.todo)
        .subscribe(
          (response) => console.log(response),
          (error) => console.log(error)
        );
      myForm.reset();
    }
    
    this.route.navigate(['/todo']);
  }
  async update(data: Todo) {
    await this.http.put('https://localhost:7115/' + 'api/todos/updatetodo', this.todo)
      .subscribe(
        (response) => console.log(response),
        (error) => console.log(error)
      );
  }
  ngOnInit() {
    this.router.params.subscribe(params => this._id = params.id);
    console.log(this._id);
    if (this._id) {
      this.http.get<Todo>('https://localhost:7115/' + 'api/todos/gettodobyid/' + this._id)
        .subscribe((data) => {
          this.todo = data;
          console.log(this.todo);
        }, (error) => {
          console.log(error)
        });
    }
  }

  
}

interface Todo {
  id: string;
  title: string;
  description: string;
  isDone: boolean;
  time: Date;
}
