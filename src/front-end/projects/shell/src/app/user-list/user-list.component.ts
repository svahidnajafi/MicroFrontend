import {Component, OnInit} from '@angular/core';
import {SharedService, UserService, UserModel, UpsertUserRequest} from 'shared';
import {MatDialog} from "@angular/material/dialog";
import {ActivatedRoute, Router} from "@angular/router";
import {UpsertUserDialogComponent} from "../upsert-user-dialog/upsert-user-dialog.component";
import {EMPTY, switchMap} from "rxjs";

@Component({
    selector: 'app-user-list',
    templateUrl: './user-list.component.html',
    styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
    data: UserModel[] = [];

    constructor(private route: ActivatedRoute, private router: Router, 
                private sharedService: SharedService, private userService: UserService, 
                private matDialog: MatDialog) {
    }

    ngOnInit(): void {
        this.data =  this.route.snapshot.data['resolvedData'];
    }
    
    reload(): void {
        this.userService.getByFilter().subscribe({
            next: (res: UserModel[]) => {
                this.data = res;
                // Reloading the user details in case it's open on the side nav
                if (this.sharedService.isSideNaveOpened && this.sharedService.userId) {
                    this.sharedService.loadUserDetails(this.sharedService.userId);
                }
            },
            error: err => {
                // TODO: Add better notification
                alert('Failed to delete the user.');
                console.error(err);
            }
        });
    }
    
    create(): void {
        this.openUpsertDialog();
    }

    edit(user: UserModel): void {
        this.openUpsertDialog(user);
    }
    
    openUpsertDialog(user?: UserModel): void {
        const isInsert = !user;
        console.log('isInsert', isInsert);
        // constructing the required data and opening the upsert dialog
        this.matDialog.open(UpsertUserDialogComponent, {data: user}).afterClosed()
            .pipe(switchMap((model?: UpsertUserRequest) => model ? this.userService.upsert(model) : EMPTY))
            // performing the upsert operation
            .subscribe({
                next: (response: string) => {
                    // Showing success notification and reloading the list
                    const messageText = `User ${isInsert ? 'added' : 'modified' } successfully.`;
                    alert(messageText);
                    this.reload();
                },
                error: (err) => {
                    // Showing the failure notification
                    const messageText = `Failed to ${isInsert ? 'add' : 'modify' } the phone number.`;
                    alert(messageText);
                }
            });
    }

    delete(user: UserModel): void {
        const confirmResult = confirm('Do you want to delete the user?');
        if (confirmResult) {
            this.userService.delete(user.id).subscribe({
                next: (res: string) => alert('user deleted successfully.'),
                error: err => {
                    // TODO: Add bette notification
                    alert('Failed to delete the user.');
                    console.error(err);
                }
            });
        }
    }

    details(user: UserModel): void {
        this.sharedService.userId = user.id;
        console.log('user id shell', this.sharedService.userId);
        this.router.navigate(['', { outlets: { details: [ 'user-details'] }}]);
        this.sharedService.loadUserDetails(user.id);
    }
}
