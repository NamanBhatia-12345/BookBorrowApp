// api.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private apiUrl = 'https://localhost:44320/api/Book';

  constructor(private http: HttpClient) { }

  getBooks(filterOn: string, filterQuery: string): Observable<any> {
    let url = `${this.apiUrl}?filterOn=${filterOn}&filterQuery=${filterQuery}`;
    return this.http.get<any>(url, { responseType: 'json' });
  }
  
  addBook(book: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, book, { responseType: 'json' });
  }
  deleteBook(bookId: number): Observable<any> {
    let url = `${this.apiUrl}/${bookId}`;
    return this.http.delete<any>(url, { responseType: 'json' });
  }
  editBook(bookId: number, updatedBook: any): Observable<any> {
    const url = `${this.apiUrl}/${bookId}`;
    return this.http.put<any>(url, updatedBook);
  }

}
