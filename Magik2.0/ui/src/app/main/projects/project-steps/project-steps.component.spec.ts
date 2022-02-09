import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectStepsComponent } from './project-steps.component';

describe('ProjectStepsComponent', () => {
  let component: ProjectStepsComponent;
  let fixture: ComponentFixture<ProjectStepsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProjectStepsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectStepsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
