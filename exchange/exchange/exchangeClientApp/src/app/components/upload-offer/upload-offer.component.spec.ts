import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadOfferComponent } from './upload-offer.component';

describe('UploadOfferComponent', () => {
  let component: UploadOfferComponent;
  let fixture: ComponentFixture<UploadOfferComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UploadOfferComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UploadOfferComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
