import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FileUploadModule } from 'ng2-file-upload';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    TabsModule,
    ToastrModule.forRoot({
      positionClass:'toast-bottom-right'
    }), // ToastrModule added
    NgxSpinnerModule.forRoot({
      type:'line-scale-party'
    }),
    BsDropdownModule.forRoot(),
    FileUploadModule
  ],
  exports:[
    BsDropdownModule,
    ToastrModule,
    TabsModule,
    NgxSpinnerModule,
    FileUploadModule
  ],
})
export class SharedModule { }
