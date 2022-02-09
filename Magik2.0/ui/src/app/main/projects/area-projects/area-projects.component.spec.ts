import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AreaProjectsComponent } from './area-projects.component';

describe('AreaProjectsComponent', () => {
  let component: AreaProjectsComponent;
  let fixture: ComponentFixture<AreaProjectsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AreaProjectsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AreaProjectsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
