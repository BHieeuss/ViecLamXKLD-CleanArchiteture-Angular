import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlogsCreateComponent } from './blogs-create.component';

describe('BlogsCreateComponent', () => {
  let component: BlogsCreateComponent;
  let fixture: ComponentFixture<BlogsCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BlogsCreateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BlogsCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
