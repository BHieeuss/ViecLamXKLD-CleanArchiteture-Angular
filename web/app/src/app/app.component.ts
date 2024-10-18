import { Component, OnInit } from '@angular/core';
import { BlogService } from './blog.service';  // Import BlogService
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';


@Component({
  selector: 'app-root',
  template: `
    <router-outlet></router-outlet>
  `,
  standalone: true,
  imports: [RouterOutlet, CommonModule],
  providers: [BlogService]  
})
export class AppComponent implements OnInit {
  blogs: any[] = [];

  // Inject BlogService vào constructor
  constructor(private blogService: BlogService) {}


  ngOnInit() {
    
  }
}
