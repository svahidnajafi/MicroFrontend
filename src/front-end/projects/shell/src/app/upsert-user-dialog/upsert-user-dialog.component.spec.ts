import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpsertUserDialogComponent } from './upsert-user-dialog.component';

describe('UpsertUserDialogComponent', () => {
  let component: UpsertUserDialogComponent;
  let fixture: ComponentFixture<UpsertUserDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpsertUserDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpsertUserDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
