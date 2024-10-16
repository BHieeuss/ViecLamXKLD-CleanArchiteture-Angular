import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlogsDeleteComponent } from './blogs-delete.component';

describe('BlogsDeleteComponent', () => {
  let component: BlogsDeleteComponent;
  let fixture: ComponentFixture<BlogsDeleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BlogsDeleteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BlogsDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
