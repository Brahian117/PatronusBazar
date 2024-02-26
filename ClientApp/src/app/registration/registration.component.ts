import { Component } from '@angular/core';

@Component({
  selector: 'registration',
  templateUrl: './registration.component.html',
//  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
  user = { UserId: 0, Name: '', Phone: 0, Email: '', HogwartsHouse: '', Username: '', Password: '' };

  constructor() { }

  onSubmit() {
   
   
  }
}
