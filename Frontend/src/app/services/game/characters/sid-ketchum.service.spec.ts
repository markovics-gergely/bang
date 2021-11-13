import { TestBed } from '@angular/core/testing';

import { SidKetchumService } from './sid-ketchum.service';

describe('SidKetchumService', () => {
  let service: SidKetchumService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SidKetchumService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
