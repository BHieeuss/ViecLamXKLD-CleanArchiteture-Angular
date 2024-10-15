import { Component, OnInit } from '@angular/core';
import { BlogService } from '../blog.service';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [BlogService, provideHttpClient()]
})
export class HomeComponent implements OnInit {
  blogs: any[] = [];

  constructor(private blogService: BlogService) {}

  ngOnInit(): void {
    this.blogService.getAllBlogs().subscribe(
      (data) => {
        this.blogs = data;
      },
      (error) => {
        console.error('Error fetching blogs:', error);
      }
    );
  }
}
