import { TestBed } from '@angular/core/testing';

import { HubBuilderService } from './hub-builder.service';

describe('HubBuilderService', () => {
  let service: HubBuilderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HubBuilderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
