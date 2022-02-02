import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileTestsComponent } from './profile-tests.component';

describe('ProfileTestsComponent', () => {
  let component: ProfileTestsComponent;
  let fixture: ComponentFixture<ProfileTestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileTestsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileTestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
