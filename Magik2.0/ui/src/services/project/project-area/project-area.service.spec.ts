import { TestBed } from '@angular/core/testing';

import { ProjectAreaService } from './project-area.service';

describe('ProjectAreaService', () => {
  let service: ProjectAreaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProjectAreaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
