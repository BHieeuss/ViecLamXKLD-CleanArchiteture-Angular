import { Component, OnInit } from '@angular/core';
import { LayoutComponent } from './layout/layout.component';
import { BlogService } from './blog.service';  // Import BlogService
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-root',
  template: `
    <app-layout></app-layout> 
  `,
  standalone: true,
  imports: [LayoutComponent, CommonModule],
  providers: [BlogService]  
})
export class AppComponent implements OnInit {
  blogs: any[] = [];

  // Inject BlogService vào constructor
  constructor(private blogService: BlogService) {}


  ngOnInit() {
    this.blogService.getAllBlogs().subscribe((data) => {
      this.blogs = data;
    }, (error) => {
      console.error('Error fetching blogs:', error);
    });
  }
}
