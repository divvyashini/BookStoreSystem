import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Post } from 'src/app/models/post.model';
import { BookStoreService } from 'src/app/services/book-store.service';

@Component({
  selector: 'app-book-store',
  templateUrl: './book-store.component.html',
  styleUrls: ['./book-store.component.css']
})
export class BookStoreComponent implements OnInit {

  posts: Post[] =[];
  areTagsEmpty: boolean = false;
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

  /**
 * Retrieves all posts which are sorted in the specified direction by the specified field
 * and contains atleast one tag specified in the input field
 * @returns The boolean value to validate tags field
 */
  getPostsForTags(): void {
    const formValues = this.postsForm.value;
    if (!formValues.tags.trim()) {
      this.areTagsEmpty = true; 
      return; 
    }
    this.posts = [];
    this.areTagsEmpty = false;
    const tagsArray = formValues.tags.split(',').map((tag: string) => tag.trim());
    this.bookStoreService.getPostsByTags(tagsArray, formValues.sortBy, formValues.direction)
      .subscribe(
        response => {
          this.posts = response;
        },
        (error) => {
          console.error('Error fetching posts with the tags:', error);
        }
      );
   
  }

  /**
 * Tracks the change event for tags input field.
 * @param tags string value specifies list of tags.
 * @returns The boolean value to validate tags field
 */
  tagsInputChange(tags: string): void {
    if (this.areTagsEmpty && tags.trim()) {
      this.areTagsEmpty = false;
    }
  }
}


