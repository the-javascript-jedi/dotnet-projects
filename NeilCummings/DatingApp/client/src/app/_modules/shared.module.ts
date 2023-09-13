import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ToastrModule.forRoot({
      positionClass:'toast-bottom-right'
    }), // ToastrModule added
    BsDropdownModule.forRoot()
  ],
  exports:[
    BsDropdownModule,
    ToastrModule
  ],
})
export class SharedModule { }
