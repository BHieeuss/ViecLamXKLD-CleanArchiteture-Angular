import { BlogService } from '../../blog.service';
import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl:'./home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [BlogService]
})
export class HomeComponent implements OnInit {
  blogs: any[] = [];

  constructor(private blogService: BlogService, private cdr: ChangeDetectorRef, private router: Router) {}

  ngOnInit(): void {
    this.blogService.getAllBlogs().subscribe({
      next: (response: any) => {
        this.blogs = response.data;
        this.cdr.detectChanges();
      },  
      error: (error) => {
        console.error('Error fetching blogs:', error);
      },
      complete: () => {
        console.log('Fetch complete');
      }
    }); 
}
viewBlog(blogId: number): void {
  // Sử dụng Router để điều hướng đến trang chi tiết của blog
  this.router.navigate(['/blog', blogId]);
}
}

