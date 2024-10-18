import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlogsManagerComponent } from './blogs-manager.component';

describe('BlogsManagerComponent', () => {
  let component: BlogsManagerComponent;
  let fixture: ComponentFixture<BlogsManagerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BlogsManagerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BlogsManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
