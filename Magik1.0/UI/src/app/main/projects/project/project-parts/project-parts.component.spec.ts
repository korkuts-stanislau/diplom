import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectPartsComponent } from './project-parts.component';

describe('ProjectPartsComponent', () => {
  let component: ProjectPartsComponent;
  let fixture: ComponentFixture<ProjectPartsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProjectPartsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectPartsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
