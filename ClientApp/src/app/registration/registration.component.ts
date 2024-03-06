import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

import Swal from 'sweetalert2';

@Component({
  selector: 'registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {



  constructor(private router: Router,private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }

  onSubmit(form: any) {
    //const User = form.value;
    const User = {
      UserId: 0,
      Name: form.value.Name,
      Phone: form.value.Phone,
      Email: form.value.Email,
      HogwartsHouse: form.value.HogwartsHouse,
      Username: form.value.Username,
      Password: form.value.Password
    };
    this.http.post(this.baseUrl + 'user', User).subscribe(
      (response) => {

        Swal.fire({
          title: '¡Registration done!',
          text: 'Now you can login',
          icon: 'success',
          confirmButtonText: '¡Ok!'
        }).then((result) => {
          if (result.isConfirmed) {
            this.router.navigateByUrl('/login');

          
          }
        });
        },
        (error) => {
          console.error('Error:', error);
        }
      );
  }
}

