import { TestBed } from '@angular/core/testing';

import { JesseJonesService } from './jesse-jones.service';

describe('JesseJonesService', () => {
  let service: JesseJonesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(JesseJonesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
