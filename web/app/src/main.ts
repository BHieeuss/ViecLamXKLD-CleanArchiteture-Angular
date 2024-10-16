import { bootstrapApplication } from '@angular/platform-browser';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { AppComponent } from './app/app.component';
import { registerLocaleData } from '@angular/common';
import { importProvidersFrom, LOCALE_ID } from '@angular/core';
import localeVi from '@angular/common/locales/vi';
import { HomeComponent } from './app/home/home.component';
import { BlogDetailComponent } from './app/blog-detail/blog-detail.component';
import { provideRouter } from '@angular/router';
import { LayoutComponent } from './app/layout/layout.component';
import { LoginComponent } from './app/login/login.component';
import { AdminPanelComponent } from './app/nav-admin/admin-panel.component';
import { AuthGuard } from './app/auth.guard';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { BlogsManagerComponent } from './app/Blogs/blogs-manager/blogs-manager.component';

registerLocaleData(localeVi, 'vi');

const routes = [
  {path: '', component: LayoutComponent},
  {path: 'blog/:id', component: BlogDetailComponent},
  {path: 'login', component: LoginComponent},
  {path: 'admin', component: AdminPanelComponent, canActivate: [AuthGuard], children:
    [
      { path: 'blogsmanager', component: BlogsManagerComponent }
    ]
  },
];
bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(),
    { provide: LOCALE_ID, useValue: 'vi-VN' },
    provideRouter(routes), provideAnimationsAsync()
  ]
}).catch(err => console.error(err));
