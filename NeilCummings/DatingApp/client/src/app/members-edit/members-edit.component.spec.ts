import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MembersEditComponent } from './members-edit.component';

describe('MembersEditComponent', () => {
  let component: MembersEditComponent;
  let fixture: ComponentFixture<MembersEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MembersEditComponent]
    });
    fixture = TestBed.createComponent(MembersEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
