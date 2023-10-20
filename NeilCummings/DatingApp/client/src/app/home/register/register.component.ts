import { Component,EventEmitter,Input,Output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
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
  registerForm:FormGroup=new FormGroup({})

  constructor(private accountService:AccountService,private toastr:ToastrService){}

  ngOnInit(){
    this.initializeForm();
  }

  initializeForm(){
    this.registerForm=new FormGroup({
      username:new FormControl("",Validators.required),
      password:new FormControl("",[Validators.required,Validators.minLength(4),Validators.maxLength(8)]),
      confirmPassword:new FormControl("",[Validators.required,this.matchValues('password')]),
    });
    // update validity of check confirmPassword for confirm password and password are the same
    this.registerForm.controls['password'].valueChanges.subscribe({
      next:()=>this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    })
  }

  matchValues(matchTo:string):ValidatorFn{
    return (control:AbstractControl)=>{
      return control.value==control.parent?.get(matchTo)?.value?null:{notMatching:true}
    }
  }

  register(){
    console.log("this.registerForm?.value",this.registerForm?.value);
    // this.accountService.register(this.model).subscribe({
    //   next:()=>{
    //     this.cancel();
    //   },
    //   error:error=>this.toastr.error(error.error.title)
    // })
  }
  
  cancel(){
    this.cancelRegister.emit(false);
  }
}
