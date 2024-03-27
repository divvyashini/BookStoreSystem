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

  /**
 * Retrieves posts from the API based on specified tags, sorting  field and direction.
 * @param tags An array of tags to filter the posts.
 * @param sortBy The field for sorting.
 * @param direction The direction of sorting (ascending or descending).
 * @returns An Observable returning an array of Post objects retrieved from the API.
 */
   getPostsByTags(tags: string[], sortBy: string, direction: string): Observable<any> {
    const params = {
      tags: tags,
      sortBy,
      direction
    };
    return this.http.get<Post[]>(`${this.baseUrl}/Books/posts`, { params });
  }
}



