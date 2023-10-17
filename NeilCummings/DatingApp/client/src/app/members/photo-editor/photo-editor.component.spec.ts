import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhotoEditorComponent } from './photo-editor.component';

describe('PhotoEditorComponent', () => {
  let component: PhotoEditorComponent;
  let fixture: ComponentFixture<PhotoEditorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PhotoEditorComponent]
    });
    fixture = TestBed.createComponent(PhotoEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
