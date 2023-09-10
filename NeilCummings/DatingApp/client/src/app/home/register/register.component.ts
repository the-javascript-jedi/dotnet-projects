import { Component,EventEmitter,Input,Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  model:any={}
  @Output() cancelRegister=new EventEmitter();

  constructor(private accountService:AccountService,private toastr:ToastrService){}

  ngOnInit(){}

  register(){
    // console.log("this.model",this.model);
    this.accountService.register(this.model).subscribe({
      next:()=>{
        this.cancel();
      },
      error:error=>this.toastr.error(error.error.title)
    })
  }
  
  cancel(){
    this.cancelRegister.emit(false);
  }
}
