import { Component,Input } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  model:any={}
  @Input() usersFromHomeComponent:any;

  constructor(){}

  ngOnInit(){}

  register(){
    console.log("this.model",this.model);
  }
  
  cancel(){
    console.log("cancelled");
  }
}
