import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  model:any={};
  loggedIn=false;
  constructor(private accountService:AccountService){}

  ngOnInit(){}

  login(){
    this.accountService.login(this.model).subscribe({
      next:(response)=>{
        console.log("response",response);
        this.loggedIn=true;
      },
      error:(error)=>{
        console.log("error",error);
      }
    })
    console.log("this.model",this.model);
  }

  logout(){
    this.loggedIn=false;
  }
}
