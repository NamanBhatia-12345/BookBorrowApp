import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginComponent } from './components/login/login.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { HomeDashboardComponent } from './components/home-dashboard/home-dashboard.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BookDetailsComponent } from './components/book-details/book-details.component';
import { interceptorFnProviders } from './interceptors/interceptor-fn';
import { UserDashboardComponent } from './components/user-dashboard/user-dashboard.component';
import { CreateBooksComponent } from './components/create-books/create-books.component';
import { EditBookComponent } from './components/edit-book/edit-book.component';
import { BorrowedBooksComponent } from './components/borrowed-books/borrowed-books.component';
import { UserDetailsComponent } from './components/user-details/user-details.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    SignUpComponent,
    HomeDashboardComponent,
    BookDetailsComponent,
    UserDashboardComponent,
    CreateBooksComponent,
    EditBookComponent,
    BorrowedBooksComponent,
    UserDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [interceptorFnProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
