import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent {
  member:Member|undefined;
  constructor(private memberService:MembersService,private route:ActivatedRoute){ }

  ngOnInit(){
    this.loadMember();
  }

  loadMember(){
    var username=this.route.snapshot.paramMap.get('username');
    console.log("username",username)
    if(!username) return;
    this.memberService.getMember(username).subscribe({
      next:member=>this.member=member
    })
  }
}
