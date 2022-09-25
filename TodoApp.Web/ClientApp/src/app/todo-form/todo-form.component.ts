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
  onSubmit(myForm: NgForm) {
    console.log("Your form data:", myForm.value);
    this.todo = myForm.value;
    console.log(this.todo);
    this.http.post('https://localhost:7115/' + 'api/todos/createtodo', this.todo)
      .subscribe(
        (response) => console.log(response),
        (error) => console.log(error)
    );
    myForm.reset();
  }

  ngOnInit() {
    this.router.params.subscribe(params => this.id = params.id);
    console.log(this.id);
    if (this.id) {
      this.http.get<Todo>('https://localhost:7115/' + 'api/todos/gettodobyid/' + this.id)
        .subscribe((data) => {
          this.todo = data;
          this.myForm?.value({
            title: this.todo.title
          });
          console.log(this.todo);
        }, (error) => {
          console.log(error)
        })
     
      
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
