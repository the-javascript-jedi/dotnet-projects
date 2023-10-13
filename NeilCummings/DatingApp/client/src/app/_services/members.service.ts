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

  getMembers(){
    return this.http.get<Member[]>(this.baseUrl+'users',this.getHttpOptions());
  }

  getMember(username:string){
    return this.http.get<Member>(this.baseUrl+'users/'+username,this.getHttpOptions());
  }

  // pass the authorization token
  getHttpOptions(){
    const userString=localStorage.getItem('user');
    if(!userString) return;
    const user=JSON.parse(userString);
    console.log("user",user)
    return {
      headers:new HttpHeaders({
        Authorization:'Bearer '+user.token
      })
    }
  }
}
