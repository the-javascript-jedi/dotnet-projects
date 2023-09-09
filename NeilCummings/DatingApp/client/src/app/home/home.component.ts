import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  registerMode=false;
  users:any;

  constructor(private http:HttpClient){}

  ngOnInit(){
   this.getUsers();
  }

  registerToggle(){
    this.registerMode=!this.registerMode;
  }

   getUsers(){
    this.http.get('https://localhost:5001/api/users').subscribe({
    next:(response)=>{
      this.users=response
    },
    error:(error)=>{
      console.log("error",error);
    },
    complete:()=>{
      console.log("request has completed");
    }
   }) 
  }
}
