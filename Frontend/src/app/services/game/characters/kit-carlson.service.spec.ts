import { TestBed } from '@angular/core/testing';

import { KitCarlsonService } from './kit-carlson.service';

describe('KitCarlsonService', () => {
  let service: KitCarlsonService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(KitCarlsonService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
