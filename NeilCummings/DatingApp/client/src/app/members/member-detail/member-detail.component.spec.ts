import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberDetailComponent } from './member-detail.component';

describe('MemberDetailComponent', () => {
  let component: MemberDetailComponent;
  let fixture: ComponentFixture<MemberDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MemberDetailComponent]
    });
    fixture = TestBed.createComponent(MemberDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
