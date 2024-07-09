import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { HomeDashboardComponent } from './components/home-dashboard/home-dashboard.component';
import { BookDetailsComponent } from './components/book-details/book-details.component';
import { UserDashboardComponent } from './components/user-dashboard/user-dashboard.component';
import { RouteGuardGuard } from './guards/route-guard.guard';
import { CreateBooksComponent } from './components/create-books/create-books.component';
import { EditBookComponent } from './components/edit-book/edit-book.component';
import { BorrowedBooksComponent } from './components/borrowed-books/borrowed-books.component';

const routes: Routes = [
  {
    path:'login',
    component:LoginComponent
  },
  {
    path:'sign-up',
    component:SignUpComponent
  },
  {
    path:'',
    component:HomeDashboardComponent
  },
  {
    path: 'book-details/:id', 
    component: BookDetailsComponent , 
  },
  {
    path:'user-dashboard',
    component: UserDashboardComponent,
    canActivate: [RouteGuardGuard],
  },
  {
    path:'create-book',
    component:CreateBooksComponent,
    canActivate: [RouteGuardGuard],
  },
  {
    path: 'edit-book/:id', 
    component:EditBookComponent,
    canActivate: [RouteGuardGuard],
  },
  {
    path:'borrowed-books',
    component:BorrowedBooksComponent,
    canActivate: [RouteGuardGuard],
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
