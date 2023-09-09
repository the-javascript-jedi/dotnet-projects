import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  model:any={};
  // currentUser$:Observable<User|null>=of(null)
  loggedIn=false;
  constructor(public accountService:AccountService){}

  ngOnInit(){
    // set the currentUser$ observale from the logged in the 
    // this.currentUser$=this.accountService.currentUser$;
  }

  login(){
    this.accountService.login(this.model).subscribe({
      next:(response)=>{
        console.log("response",response);
      },
      error:(error)=>{
        console.log("error",error);
      }
    })
    console.log("this.model",this.model);
  }

  logout(){
    this.accountService.logout()
  }
}
