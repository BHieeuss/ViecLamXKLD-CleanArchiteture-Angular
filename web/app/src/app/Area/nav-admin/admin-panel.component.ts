import { Component, ViewChild, AfterViewInit } from '@angular/core';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { NavigationEnd, Router, RouterModule } from '@angular/router';
import { filter } from 'rxjs';
import { SidenavService } from '../../Service/MatSidenav';

@Component({
  selector: 'app-admin-panel',
  standalone: true,
  imports: [MatIconModule, MatSidenavModule, MatListModule, MatToolbarModule, RouterModule],
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent{
  @ViewChild('drawer') drawer!: MatSidenav;

  constructor(private sidenavService: SidenavService) {}

  ngAfterViewInit() {
    this.sidenavService.setSidenav(this.drawer);  // Set Sidenav instance to the service
  }

  toggleDrawer() {
    this.sidenavService.toggle();  // Sử dụng service để toggle
  }
}