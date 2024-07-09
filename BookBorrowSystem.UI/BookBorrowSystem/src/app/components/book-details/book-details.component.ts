import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']
})
export class BookDetailsComponent implements OnInit {
  bookDetails: any;
  constructor(private route: ActivatedRoute, private http: HttpClient) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const bookId = params['id'];

      
      this.http.get<any>(`https://localhost:44320/api/Book/${bookId}`).subscribe(
        (response) => {
          this.bookDetails = response; 
        },
        (error) => {
          console.error('Error fetching book details:', error);
        }
      );
    });
  }
}
