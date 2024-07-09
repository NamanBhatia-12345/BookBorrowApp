import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-create-books',
  templateUrl: './create-books.component.html',
  styleUrls: ['./create-books.component.css']
})
export class CreateBooksComponent {
  book: any = {};

  constructor(private bookService: BookService, private router: Router) { }

  addBook(): void {
    if (!this.validateForm()) {
      return;
    }
    this.bookService.addBook(this.book).subscribe(
      (response: any) => {
        console.log('Book added successfully:', response);
        this.resetForm();
        this.router.navigate(['/user-dashboard']);
      },
      (error) => {
        console.error('Error adding book:', error);
      }
    );
  }

  validateForm(): boolean {
    return !!this.book.name && !!this.book.rating && !!this.book.author && !!this.book.genre && !!this.book.description;
  }



  resetForm(): void {
    this.book = {};
  }

}
