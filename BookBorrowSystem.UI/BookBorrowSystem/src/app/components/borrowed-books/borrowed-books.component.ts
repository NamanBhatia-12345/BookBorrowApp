import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-borrowed-books',
  templateUrl: './borrowed-books.component.html',
  styleUrls: ['./borrowed-books.component.css']
})
export class BorrowedBooksComponent {


  borrowedBooks: any[] = [];

  constructor(private http: HttpClient,private auths:AuthService) { }

  ngOnInit(): void {
    this.getBorrowedBooks();
  }

  getBorrowedBooks(): void {
    const apiUrl = 'https://localhost:44320/api/BorrowBook/GetAllBorrowBooksByUser';
    const token = this.auths.takeToken();
    console.log(token);

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });

    this.http.get(apiUrl, { headers }).subscribe(
      (response: any) => {
        this.borrowedBooks = response;
      },
      (error) => {
        console.error('Error fetching borrowed books:', error);
      }
    );
  }

  
  returnBook(bookId: number): void {
    const returnUrl = `https://localhost:44320/api/BorrowBook/ReturnBook/${bookId}`;
  
    const token = this.auths.takeToken();
  
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  
    this.http.get(returnUrl, { headers, observe: 'response', responseType: 'text' }).subscribe(
      (response) => {
        if (response.body && response.headers.get('content-type')?.toLowerCase().includes('application/json')) {} 
        else {
        alert('Book returned successfully!');
          location.reload();}
      },
      (error) => {
        console.error(`Error returning book with ID ${bookId}:`, error);
        if (error instanceof HttpErrorResponse) {
          console.error('Status:', error.status);
          console.error('Body:', error.error);
        }
        alert('Error returning book. Please try again.');
      }
    );
  }
  

}
