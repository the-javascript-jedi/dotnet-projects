import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberListComponent } from './member-list.component';

describe('MemberListComponent', () => {
  let component: MemberListComponent;
  let fixture: ComponentFixture<MemberListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MemberListComponent]
    });
    fixture = TestBed.createComponent(MemberListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
