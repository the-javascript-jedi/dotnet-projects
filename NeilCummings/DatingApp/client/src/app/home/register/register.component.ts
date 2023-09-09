import { Component,EventEmitter,Input,Output } from '@angular/core';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  model:any={}
  @Output() cancelRegister=new EventEmitter();

  constructor(private accountService:AccountService){}

  ngOnInit(){}

  register(){
    // console.log("this.model",this.model);
    this.accountService.register(this.model).subscribe({
      next:()=>{
        this.cancel();
      },
      error:error=>console.log(error)
    })
  }
  
  cancel(){
    this.cancelRegister.emit(false);
  }
}
