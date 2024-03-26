import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Post } from '../models/post.model';

@Injectable({
  providedIn: 'root'
})
export class BookStoreService {

  private baseUrl = 'http://localhost:5132/api';

  constructor(private http:HttpClient) {

   }

   getPostsByTags(tags: string[], sortBy: string, direction: string): Observable<Post[]> {
    const params = {
      tags: tags,
      sortBy,
      direction
    };
    return this.http.get<Post[]>(`${this.baseUrl}/Books/posts`, { params });
  }
}
