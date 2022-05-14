import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LearnCardsComponent } from './learn-cards.component';

describe('LearnCardsComponent', () => {
  let component: LearnCardsComponent;
  let fixture: ComponentFixture<LearnCardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LearnCardsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LearnCardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
