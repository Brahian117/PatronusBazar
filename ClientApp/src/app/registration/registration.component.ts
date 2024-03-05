// registration.component.ts
import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {

  constructor(private http: HttpClient) { }

  onSubmit(form: any) {
    const formData = form.value;

    this.http.post('/User', formData)
      .subscribe(
        (response) => {
          console.log('Server response:', response);
        },
        (error) => {
          console.error('Error:', error);
        }
      );
  }
}
