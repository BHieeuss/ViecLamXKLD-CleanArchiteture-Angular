import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-banner-content',
  templateUrl: './banner-content.component.html',
  styleUrls: ['./banner-content.component.css'],
})
export class BannerContentComponent implements OnInit {
  constructor(private router: Router) {}

  ngOnInit(): void {}
}
