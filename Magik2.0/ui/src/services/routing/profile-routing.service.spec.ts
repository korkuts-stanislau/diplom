import { TestBed } from '@angular/core/testing';

import { ProfileRoutingService } from './profile-routing.service';

describe('ProfileRoutingService', () => {
  let service: ProfileRoutingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProfileRoutingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
