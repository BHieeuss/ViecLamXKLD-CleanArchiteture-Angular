import { CommonModule } from '@angular/common';
import { ChangeDetectorRef,Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Blog, BlogService } from '../../blog.service';
import { FooterComponent } from '../../layout/footer/footer.component';
import { HeaderComponent } from '../../layout/header/header.component';

@Component({
  selector: 'app-blog-detail',
  standalone: true,
  imports: [CommonModule, FooterComponent, HeaderComponent],
  templateUrl: './blog-detail.component.html',
  styleUrl: './blog-detail.component.css'
})
export class BlogDetailComponent implements OnInit {
  blogDetail: any;

  constructor(private route: ActivatedRoute, private cdr: ChangeDetectorRef, private blogService: BlogService) {}
  
  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.blogService.getBlogById(+id).subscribe({
        next: (response: any) => {
          console.log('Blog detail data:', response.data);
          // Kiểm tra nếu response.data là một mảng
          if (Array.isArray(response.data) && response.data.length > 0) {
            this.blogDetail = response.data[0];
          } else {
            console.error('No blog details found');
          }
        },
        error: (error) => {
          console.error('Error fetching blog details:', error);
        }
      });
    }
  }
}  