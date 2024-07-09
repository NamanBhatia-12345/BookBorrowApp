import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  constructor(private route:Router,public authservice:AuthService ) { }

  public log()
  {
    this.authservice.logOff();
    this.route.navigate(['/'])
  }
}
