import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddProjectAreaComponent } from './add-project-area.component';

describe('AddProjectAreaComponent', () => {
  let component: AddProjectAreaComponent;
  let fixture: ComponentFixture<AddProjectAreaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddProjectAreaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddProjectAreaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
