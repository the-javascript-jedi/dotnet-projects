import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl="https://localhost:5001/api/";
  // provide a globally accessible observable
  private currentUserSource=new BehaviorSubject<User|null>(null);
  currentUser$=this.currentUserSource.asObservable();

  constructor(private http:HttpClient) { }
  // when user logs in - set the status in the observable and local storage
  login(model:any){
    return this.http.post<User>(this.baseUrl+'account/login',model).pipe(map((response:User)=>{
      const user=response;
      if(user){
        localStorage.setItem('user',JSON.stringify(user));
        this.currentUserSource.next(user);
      }
    }))
  }
  // register user to the application
  register(model:any){
       return this.http.post<User>(this.baseUrl+'account/register',model).pipe(map((user)=>{
        if(user){
                localStorage.setItem('user',JSON.stringify(user));
                this.currentUserSource.next(user);
        }
       }))
  }

  setCurrentUser(user:User){
    this.currentUserSource.next(user);
  }
  //remove local storage values and current user value
  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
