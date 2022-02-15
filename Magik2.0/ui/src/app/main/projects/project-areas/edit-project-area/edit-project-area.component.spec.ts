import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditProjectAreaComponent } from './edit-project-area.component';

describe('EditProjectAreaComponent', () => {
  let component: EditProjectAreaComponent;
  let fixture: ComponentFixture<EditProjectAreaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditProjectAreaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditProjectAreaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
