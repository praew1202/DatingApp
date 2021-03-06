import { error } from '@angular/compiler/src/util';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
model : any = {};
@Output() cancelRegister = new EventEmitter();
  constructor(private accountService : AccountService) { }

  ngOnInit(): void {
  }

cancel(){
  this.cancelRegister.emit(false);
}
register(){
  this.accountService.register(this.model).subscribe((response) => {
    this.cancel();
  },error => {
    console.log(error);
  })
}
}
