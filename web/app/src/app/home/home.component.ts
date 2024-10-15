import { BlogService } from '../blog.service';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';

interface Blog
{
  id: number;
  heading?: string;
  subHeading?: string;
  poster?: string;
  blogDate: Date;
  blogDetail?: string;
  productName: string;
  productImage?: string;
  imageFile?: File; 
}
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [BlogService]
})
export class HomeComponent implements OnInit {
  blogs: any[] = [];

  constructor(private blogService: BlogService, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.blogService.getAllBlogs().subscribe({
      next: (response: any) => {
        this.blogs = response.data;
        this.cdr.detectChanges();
      },  
      error: (error) => {
        console.error('Error fetching blogs:', error);
        console.error('Status:', error.status);
        console.error('Status Text:', error.statusText);
      },
      complete: () => {
        console.log('Fetch complete');
      }
    });
}}

