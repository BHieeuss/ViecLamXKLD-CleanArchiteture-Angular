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

registerLocaleData(localeVi, 'vi');

const routes = [
  {path: '', component: LayoutComponent},
  {path: 'blog/:id', component: BlogDetailComponent}
];
bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(),
    { provide: LOCALE_ID, useValue: 'vi-VN' },
    provideRouter(routes)
  ]
}).catch(err => console.error(err));
