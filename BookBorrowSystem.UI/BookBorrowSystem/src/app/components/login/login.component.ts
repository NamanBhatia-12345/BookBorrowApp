import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  auth = {
    username: '',
    password: '',
  };
  loginError: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {}

  formSubmit() {
    console.log("LoginFormSubmitted");
    if (!this.isPasswordValid(this.auth.password)) {
      this.loginError = 'Password must be at least 5 characters long and include at least one uppercase letter, one lowercase letter, one digit, and one special character.';
      return;
    }

    this.authService.createToken(this.auth).subscribe(
      (data: any) => {
        console.log("success");
        console.log(data);
        this.authService.loggedUser(data.token);
        this.authService.defaultUser().subscribe(
          (user: any) => {
            this.authService.setLoggedInUser(user);
            console.log(user);
            this.router.navigate(['/user-dashboard']);
          },
          (userError) => {
            console.log("error retrieving user details");
            this.loginError = 'An error occurred during login. Please try again later.';
          }
        );
      },
      (error) => {
        console.log("error");
        this.loginError = 'Invalid username or password';
      }
    );
  }
  private isPasswordValid(password: string): boolean {
    const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{5,}$/;
    return passwordRegex.test(password);
  }

}
