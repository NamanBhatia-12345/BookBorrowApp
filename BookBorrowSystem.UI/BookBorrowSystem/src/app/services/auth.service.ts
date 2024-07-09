import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:44320/api/Auth';

  constructor(private http: HttpClient) { }
 
  public createToken(loginData: any): Observable<any> {
    let url = `${this.apiUrl}/Login`;
    return this.http.post(url, loginData);
  }

  public loggedUser(token: any): Observable<any> {
    sessionStorage.setItem("token", token.jwtToken);
    return of(true);
  }

  public boolLog(): boolean {
    let tokenString = sessionStorage.getItem("token");
    return !!tokenString; 
  }

  public takeToken() {
    return sessionStorage.getItem("token");
  }
  public defaultUser(): Observable<any> {
    let url = `${this.apiUrl}/GetUserDetails`;
    return this.http.get(url);
  }


  public logOff() {
    sessionStorage.removeItem("token");
    sessionStorage.removeItem("user");
    return true;
  }

  public setLoggedInUser(user: any) {
    sessionStorage.setItem('user', JSON.stringify(user));
  }

  public getLoggedInUser() {
    const userStr = sessionStorage.getItem("user");
    if (userStr !== null) {
      try {
        const user = JSON.parse(userStr);
        return user;
      } catch (error) {
        console.error("Error parsing user data:", error);
        this.logOff();
        return null;
      }
    } else {
      this.logOff();
      return null;
    }
  }

  public storeUser(): Observable<any> {
    const url = `${this.apiUrl}/GetUserDetails`;
    const headers = new HttpHeaders().set("Authorization", `Bearer ${sessionStorage.getItem('token')}`);

    return this.http.get(url, { headers }).pipe(
      tap((user: any) => {
        this.setLoggedInUser(user);
        console.log("User Details:", user);
      })
    );
  }
  public isUserAuthenticated() {
    let tokenString = sessionStorage.getItem("token");
    // console.log(tokenString);
    if (tokenString == undefined || tokenString == ' ' || tokenString == null) {
      return false;
    } else {
      return true;
    }
  }
}
