import { taggedTemplate } from '@angular/compiler/src/output/output_ast';
import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { user } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
  }
login(){
  this.accountService.login(this.model).subscribe((response) => {
    
      console.log(response);
  },error =>{
    console.log(error);
  })
}
login1(){
  this.accountService.login(this.model).subscribe({
    next :(response) =>{
      console.log(`I like ${response}`)
    },
    error : (error) => {
      console.log(error);
    },
    complete:() => {
      console.log(`ends`);
    }
  })
}

//  observerNarate = {
//   next: (idol) => {
//       console.log(`I like ${idol}`)
//   },
//   error: (err) => {
//       console.log(`I'm too drunk~`)
//   },
//   completed: () =>{
//       console.log(`I like all idols`)
//   },
// }

logout(){
  this.accountService.logout();

}
}
