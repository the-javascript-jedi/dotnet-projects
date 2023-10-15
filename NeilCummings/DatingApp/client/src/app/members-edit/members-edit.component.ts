import { Component, HostListener, ViewChild } from '@angular/core';
import { Member } from '../_models/member';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';
import { MembersService } from '../_services/members.service';
import { take } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-members-edit',
  templateUrl: './members-edit.component.html',
  styleUrls: ['./members-edit.component.css']
})
export class MembersEditComponent {
  member:Member|undefined;
  user:User|null=null;
  @ViewChild('editForm') editForm:NgForm|undefined;
  // trigger the browser feature if we try to leave the site when form is dirty
  // window:beforeunload is a browser event we listen to inside the browser
  @HostListener('window:beforeunload',['$event']) unloadNotification($event:any){
    if(this.editForm?.dirty){
      // this makes the browser to activate an alert
      $event.returnValue=true;
    }
  }

  constructor(private accountService:AccountService,private memberService:MembersService,private toastr:ToastrService){
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next:user=>this.user=user
    })
  }

  ngOnInit(){
    this.loadMember();
  }

  loadMember(){
    if(!this.user) return;
    this.memberService.getMember(this.user.username).subscribe({
      next:member=>this.member=member
    })
  }

  updateMember(){
    console.log("this.member",this.member);
    this.memberService.updateMember(this.editForm?.value).subscribe({
      next:_=>{
        this.toastr.success('Profile Updated Successfully');
        // reset form
        this.editForm?.reset(this.member);
      }
    })
  }
}
