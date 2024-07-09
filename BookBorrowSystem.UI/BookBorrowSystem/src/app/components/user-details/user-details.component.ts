import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent {
  userDetails: any = {};

  constructor(private http: HttpClient, private auth: AuthService) { }

  ngOnInit(): void {
    this.getUserDetails();
  }

  getUserDetails(): void {
    const apiUrl = 'https://localhost:44320/api/Auth/GetUserDetails';
    const token = this.auth.takeToken();

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });

    this.http.get(apiUrl, { headers }).subscribe(
      (response: any) => {
        this.userDetails = response;
      },
      (error) => {
        console.error('Error fetching user details:', error);
      }
    );
  }

}
