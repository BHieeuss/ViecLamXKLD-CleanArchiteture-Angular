import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-banner-navigation',
  templateUrl: './banner-navigation.component.html',
  styleUrls: ['./banner-navigation.component.css'],
})
export class BannerNavigationComponent implements OnInit {
  logoutStatus = false;

  constructor(private router: Router) {}

  ngOnInit(): void {}
  goToAdmin() {
    this.router.navigate(['/admin']);
  }
}
