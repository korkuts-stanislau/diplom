import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileStatisticComponent } from './profile-statistic.component';

describe('ProfileStatisticComponent', () => {
  let component: ProfileStatisticComponent;
  let fixture: ComponentFixture<ProfileStatisticComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileStatisticComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileStatisticComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
