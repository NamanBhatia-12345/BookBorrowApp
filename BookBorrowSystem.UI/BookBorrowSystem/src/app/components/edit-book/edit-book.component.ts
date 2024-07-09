import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-edit-book',
  templateUrl: './edit-book.component.html',
  styleUrls: ['./edit-book.component.css']
})
export class EditBookComponent  implements OnInit {

  @ViewChild('editBookForm') editBookForm!: NgForm; 
  bookId: any;
  bookDetails: any = {
    name: '',
    rating: 0,
    author: '',
    genre: '',
    description: '',
    isBookAvailable: true
  };

  constructor(private route: ActivatedRoute, private router: Router, private bookService: BookService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.bookId = +params['id'];
    });
  }

  editBook(): void {
    if (this.editBookForm.form.valid) {
      this.bookService.editBook(this.bookId, this.bookDetails).subscribe(
        () => {
          console.log('Book edited successfully.');
          this.router.navigate(['/user-dashboard']);
        },
        (error) => {
          console.error('Error editing book:', error);
        }
      );
    }
  }

}
