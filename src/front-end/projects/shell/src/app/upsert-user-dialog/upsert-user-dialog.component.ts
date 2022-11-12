import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {NullableUserModel, UpsertUserRequest} from "shared";

@Component({
    selector: 'app-upsert-user-dialog',
    templateUrl: './upsert-user-dialog.component.html',
    styleUrls: ['./upsert-user-dialog.component.scss']
})
export class UpsertUserDialogComponent implements OnInit {
    request: UpsertUserRequest = {
        id: null,
        name: '',
        email: ''
    };
    isInsert: boolean = true;

    constructor(public dialogRef: MatDialogRef<UpsertUserDialogComponent>,
                @Inject(MAT_DIALOG_DATA) public data: NullableUserModel) {
    }

    ngOnInit(): void {
        if (this.data) {
            this.isInsert = false;
            this.request = this.data;
        }
    }

    cancel(): void {
        this.dialogRef.close();
    }

    submit(): void {
        this.dialogRef.close(this.request);
    }
}
