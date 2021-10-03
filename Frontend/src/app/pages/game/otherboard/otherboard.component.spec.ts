import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OtherboardComponent } from './otherboard.component';

describe('OtherboardComponent', () => {
  let component: OtherboardComponent;
  let fixture: ComponentFixture<OtherboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OtherboardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OtherboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
