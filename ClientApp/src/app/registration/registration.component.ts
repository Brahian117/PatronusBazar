import { Component, Inject, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
  @ViewChild('registrationForm') registrationForm: NgForm | undefined;

  constructor(private router: Router, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    if (this.isUserAuthenticated() == true) {
      this.router.navigateByUrl('/');
    }

  }

  onSubmit(form: any) {

    if (form.invalid) {
      Swal.fire({
        title: 'Error',
        text: 'Please fill in all required fields.',
        icon: 'error',
        confirmButtonText: 'Ok'
      });
      return;
    }
    const phoneNumber = form.value.Phone;

    if (phoneNumber.length !== 10 || isNaN(phoneNumber) || !(/^\d+$/.test(phoneNumber))) {
      Swal.fire({
        title: 'Error',
        text: 'Phone number must be 10 digits & must only be numbers',
        icon: 'error',
        confirmButtonText: 'Ok'
      });
      return;
    }

    const email = form.value.Email;

    if (!/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/.test(email)) {
      Swal.fire({
        title: 'Error',
        text: 'Please enter a valid email address.',
        icon: 'error',
        confirmButtonText: 'Ok'
      });
      return;
    }

    const password = form.value.Password;

    if (!/(?=.*[a-zA-Z])(?=.*[0-9])/.test(password)) {
      Swal.fire({
        title: 'Error',
        text: 'Password must contain at least one letter and one number.',
        icon: 'error',
        confirmButtonText: 'Ok'
      });
      return;
    }
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
    this.http.post(this.baseUrl + 'account/register', User).subscribe(
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

  public logOut = () => {
    localStorage.removeItem("jwt");
  }

  public isUserAuthenticated() {
    const token = localStorage.getItem("jwt");

    if (token) {
      return true;
    }
    else {
      return false;
    }

  }
}

