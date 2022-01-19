import { TestBed } from '@angular/core/testing';

import { ProjectPartsService } from './project-parts.service';

describe('ProjectPartsService', () => {
  let service: ProjectPartsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProjectPartsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
