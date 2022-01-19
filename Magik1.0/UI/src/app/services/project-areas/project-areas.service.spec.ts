import { TestBed } from '@angular/core/testing';

import { ProjectAreasService } from './project-areas.service';

describe('ProjectAreasService', () => {
  let service: ProjectAreasService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProjectAreasService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
