import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlogsUpdateComponent } from './blogs-update.component';

describe('BlogsUpdateComponent', () => {
  let component: BlogsUpdateComponent;
  let fixture: ComponentFixture<BlogsUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BlogsUpdateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BlogsUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
