import { TestBed } from '@angular/core/testing';

import { AppRoutingService } from './routing.service';

describe('RoutingService', () => {
  let service: AppRoutingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AppRoutingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
