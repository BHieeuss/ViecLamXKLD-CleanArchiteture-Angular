import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BlogService, Blog } from '../../../blog.service';  // Import Blog interface
import { Router } from '@angular/router';  // Sửa lại import router
import { SidenavService } from '../../../Service/MatSidenav';

@Component({
  selector: 'app-blogs-manager',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './blogs-manager.component.html',
  styleUrls: ['./blogs-manager.component.css'],
  providers: [BlogService]
})
export class BlogsManagerComponent implements OnInit {
  blogs: Blog[] = [];
  newBlog: Blog = { id: 0, heading: '', subHeading: '', blogDate: new Date(), productName: '' }; 
  editIndex: number | null = null;

  constructor(private blogService: BlogService, private cdr: ChangeDetectorRef, private router: Router, private sidenavService: SidenavService) {}
    toggleMenu() {
      this.sidenavService.toggle();
    }
  ngOnInit(): void {
    console.log('Fetching blogs...');
    this.blogService.getAllBlogs().subscribe({
      next: (response: Blog[]) => {
        this.blogs = response;  // Gán danh sách blogs từ API
        console.log('Blogs fetched:', this.blogs);
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
  

  addBlog() {
    if (this.editIndex === null) {
      // Thêm blog mới
      this.blogs.push({ ...this.newBlog });
    } else {
      // Cập nhật blog
      this.blogs[this.editIndex] = { ...this.newBlog };
      this.editIndex = null;
    }
    // Reset form
    this.newBlog = { id: 0, heading: '', subHeading: '', blogDate: new Date(), productName: '' };
  }

  editBlog(index: number) {
    this.editIndex = index;
    this.newBlog = { ...this.blogs[index] };
  }

  deleteBlog(index: number) {
    this.blogs.splice(index, 1);
  }
}
