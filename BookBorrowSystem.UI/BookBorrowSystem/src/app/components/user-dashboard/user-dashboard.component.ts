import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})
export class UserDashboardComponent implements OnInit {
  books: any[] = [];
  loggedInUserId: string = '';
  constructor(private bookService: BookService,private auths:AuthService,private route:Router,private http: HttpClient) { }

  ngOnInit(): void {
    this.getAvailableBooks();
    this. loggedInUserId=this.auths.getLoggedInUser().userId;
    console.log(this. loggedInUserId);
  }

  getAvailableBooks(): void {
    this.bookService.getBooks('isBookAvailable', 'true').subscribe(
      (response: any) => {
        this.books = response;
      },
      (error) => {
        console.error('Error fetching available books:', error);
      }
    );
  }

  editBook(book: any): void {
    this.route.navigate(['/edit-book', book.id]);
  }

  deleteBook(book: any): void {
    const bookId = book.id;

    this.bookService.deleteBook(bookId).subscribe(
      () => {
        console.log('Book deleted successfully.');
        this.getAvailableBooks();
      },
      (error) => {
        console.error('Error deleting book:', error);
      }
    );
  }

  borrowBook(book: any): void {
    const borrowUrl = `https://localhost:44320/api/BorrowBook/${book.id}`;

    this.http.get(borrowUrl).subscribe(
      (response: any) => {
        console.log('Book borrowed successfully:', response);
        
        alert('Book borrowed successfully!');
        location.reload();
        
      },
      (error) => {
        console.error('Error borrowing book:', error);
        alert('Error borrowing book. Insufficient Token Available.');
      }
    );
  }

}
