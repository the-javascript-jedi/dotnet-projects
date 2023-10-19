import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl=environment.apiUrl;
  // store api response in service and check if the stored data is not empty else make a new api call
  members:Member[]=[];

  constructor(private http:HttpClient) { }
  // the auth token for the apis are added in the interceptor, so we do not need to specify it in the httpoptions
  getMembers(){
    // if data exists return the existing data
    if(this.members.length>0) return of(this.members);
    return this.http.get<Member[]>(this.baseUrl+'users').pipe(
      map(
        members=>{
          this.members=members;
          return members;
        }
      ));
  }

  getMember(username:string){
    const member=this.members.find(x=>x.userName===username);
    if(member) return of(member);

    return this.http.get<Member>(this.baseUrl+'users/'+username);
  }

  updateMember(member:Member){
    return this.http.put(this.baseUrl+'users',member).pipe(map(
      ()=>{
        const index=this.members.indexOf(member);
        this.members[index]={...this.members[index],...member}
      }
    ))
  }  
  setMainPhoto(photoId:number){
    // for put request send empty object
    return this.http.put(this.baseUrl+'users/set-main-photo/'+photoId,{});
  }
  // // pass the authorization token
  // getHttpOptions(){
  //   const userString=localStorage.getItem('user');
  //   if(!userString) return;
  //   const user=JSON.parse(userString);
  //   console.log("user",user)
  //   return {
  //     headers:new HttpHeaders({
  //       Authorization:'Bearer '+user.token
  //     })
  //   }
  // }
}
