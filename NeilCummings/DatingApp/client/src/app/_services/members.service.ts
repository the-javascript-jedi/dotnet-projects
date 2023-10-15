import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl=environment.apiUrl;
  constructor(private http:HttpClient) { }
  // the auth token for the apis are added in the interceptor, so we do not need to specify it in the httpoptions
  getMembers(){
    return this.http.get<Member[]>(this.baseUrl+'users');
  }

  getMember(username:string){
    return this.http.get<Member>(this.baseUrl+'users/'+username);
  }

  updateMember(member:Member){
    return this.http.put(this.baseUrl+'users',member)
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
