import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'; // Đảm bảo bạn import đúng HttpClient
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BlogService {
  blog: any[] = [];
  constructor(private http: HttpClient) {}

  public getAllBlogs(): Observable<any> {
    return this.http.get<any[]>('https://localhost:7168/api/BlogActions');
  }
}
