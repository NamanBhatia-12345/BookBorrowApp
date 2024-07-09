import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-home-dashboard',
  templateUrl: './home-dashboard.component.html',
  styleUrls: ['./home-dashboard.component.css']
})
export class HomeDashboardComponent implements OnInit {
  searchQuery: string = '';
  books: any[] = [];

  constructor(private bookService: BookService, private router:Router) { }

  ngOnInit(): void {
    this.searchBooks('name');
  }


  showDetails(book: any) {
    const bookId = book.id; 
    this.router.navigate(['/book-details', bookId]);
  }

  searchBooks(filterOn: string): void {
    let filterQuery: string = '';
    if (filterOn === 'custom') {
      filterQuery = this.searchQuery;
    }
    this.bookService.getBooks(filterOn, filterQuery).subscribe(
      (response: any[]) => {
        console.log('API Response:', response);
        this.books = response;
      },
      (error) => {
        console.error('Error fetching books:', error);
      }
    );
  }
}
