import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';

export interface Blog
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
@Injectable({
  providedIn: 'root'
})
export class BlogService {
  blog: any[] = [];
  private apiUrl = 'https://localhost:7168/api/BlogActions'; 
  constructor(private http: HttpClient) {}

  getAllBlogs(): Observable<Blog[]>{
    return this.http.get<Blog[]>(this.apiUrl); 
  }
  getBlogById(id: number): Observable<Blog> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Blog>(url);
  }
}
