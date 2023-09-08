import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './_models/user';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'client';
  users:any;
  constructor(private http:HttpClient,private accountService:AccountService){
  }
  
  ngOnInit(): void {
   this.getUsers();
   this.setCurrentUser();
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

  setCurrentUser(){
    // JSON.parse(localStorage.getItem('user')!) - the ! at the end will overide type safety
    // const user:User=JSON.parse(localStorage.getItem('user')!);
    //access stored values from local storage
    const userString=localStorage.getItem('user');
    if(!userString) return;
    const user:User = JSON.parse(userString);
    this.accountService.setCurrentUser(user);
  }
}
