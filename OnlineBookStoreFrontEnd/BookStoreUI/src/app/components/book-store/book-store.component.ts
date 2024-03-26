import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BookStoreService } from 'src/app/services/book-store.service';

@Component({
  selector: 'app-book-store',
  templateUrl: './book-store.component.html',
  styleUrls: ['./book-store.component.css']
})
export class BookStoreComponent implements OnInit {


  
  posts: any[] =[];

  postsForm: FormGroup; 


  constructor(private formBuilder: FormBuilder,private bookStoreService: BookStoreService) {

    this.postsForm = this.formBuilder.group({
      postId: ['', [Validators.required]],
      tags: [''],
      sortBy: [''],
      direction: ['']
    });
  }

  ngOnInit(): void {
  }

  getPostsForTags(): void {
    const formValues = this.postsForm.value;
    const tagsArray = formValues.tags.split(',').map((tag: string) => tag.trim());
    this.bookStoreService.getPostsByTags(tagsArray, formValues.sortBy, formValues.direction)
      .subscribe(
        (response) => {
          this.posts = response;
        },
        (error) => {
          console.error('Error fetching posts for the tags:', error);
        }
      );
   
  }
}
