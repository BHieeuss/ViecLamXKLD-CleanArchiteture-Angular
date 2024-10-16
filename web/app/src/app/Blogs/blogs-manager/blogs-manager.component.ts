import { Component } from '@angular/core';
import { AdminPanelComponent } from '../../nav-admin/admin-panel.component';

@Component({
  selector: 'app-blogs-manager',
  standalone: true,
  imports: [AdminPanelComponent],
  templateUrl: './blogs-manager.component.html',
  styleUrl: './blogs-manager.component.css'
})
export class BlogsManagerComponent {

}
