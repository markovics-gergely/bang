import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OwnboardComponent } from './ownboard.component';

describe('OwnboardComponent', () => {
  let component: OwnboardComponent;
  let fixture: ComponentFixture<OwnboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OwnboardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OwnboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
