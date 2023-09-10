import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  model:any={};
  // currentUser$:Observable<User|null>=of(null)
  loggedIn=false;
  constructor(public accountService:AccountService,private router:Router,private toastr:ToastrService){}

  ngOnInit(){
    // set the currentUser$ observale from the logged in the 
    // this.currentUser$=this.accountService.currentUser$;
  }

  login(){
    this.accountService.login(this.model).subscribe({
      next:(response)=>{
        console.log("response",response);
        this.router.navigateByUrl('/members');
      },
      error:(error)=>{
        console.log("error",error);
        // display error as a toast message
        this.toastr.error(error.error.title);
        this.router.navigateByUrl('/');
      }
    })
    console.log("this.model",this.model);
  }

  logout(){
    this.accountService.logout()
  }
}
