import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ObservableInput, ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators';
import { user } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseURL = "https://localhost:5001/api/"
  private currentUserSource = new ReplaySubject<user>(1);
  currentUser$ = this.currentUserSource.asObservable()
  constructor(private http : HttpClient) { 

  }

  login(model:any) : Observable<user>{
    return this.http.post(this.baseURL+"account/login",model).pipe(
      map((response : user) =>{
        const user = response;
        if(user){
          localStorage.setItem("user" ,JSON.stringify(user));
          this.currentUserSource.next(user);
        }
        return user;
      })
    )
  }
  register(model : any){
    return this.http.post(this.baseURL+"account/register",model).pipe(
      map((response : user) =>{
        const user = response;
        if(user){
          localStorage.setItem("user" ,JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }
  setCurrentUser(user : user){
    this.currentUserSource.next(user);
  }

  logout(){
    localStorage.removeItem("user");
    this.currentUserSource.next(null);
  }


}
