import { Component, Inject, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';


@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],

})
export class LoginComponent {
  @ViewChild('loginForm') loginForm: NgForm | undefined;

  constructor(private router: Router, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    if (this.isUserAuthenticated()==true) {
      this.router.navigateByUrl('/');
    }
   
   
  }

  onSubmit(form: any) {
    if (form.invalid) {
      Swal.fire({
        title: 'Error',
        text: 'Please enter username and password.',
        icon: 'error',
        confirmButtonText: 'Ok'
      });
      return;
    }
    const User = {
      UserId: 0,
      Name: '',
      Phone: 0,
      Email: '',
      HogwartsHouse: '',
      Username: form.value.Username,
      Password: form.value.Password
    };

    this.http.post(this.baseUrl + 'account/login', User).subscribe(
      (response: any) => {
        if (response.mess == "OK") {

          localStorage.setItem("jwt", response.token);
          localStorage.setItem('name', response.name);
          localStorage.setItem('hogwartsHouse', response.hogwartsHouse);
          this.router.navigateByUrl('/');

        } else {
          Swal.fire({
            title: '¡Error!',
            text: response.mess,
            icon: 'error',
            confirmButtonText: '¡Ok!'
          });
        }
      },
      (error) => {
        Swal.fire({
          title: '¡Error!',
          text: 'Try again or contact admin',
          icon: 'error',
          confirmButtonText: '¡Ok!'
        });
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
