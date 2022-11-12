import {Component, OnDestroy, OnInit} from '@angular/core';
import {NullableUserModel, SharedService, UserService} from 'shared';
import {Subject, takeUntil} from "rxjs";

@Component({
    selector: 'app-user',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit, OnDestroy {
    
    user: NullableUserModel = null;
    private unsubscribe$ = new Subject<void>();
    constructor(private sharedService: SharedService, private userService: UserService) {}

    ngOnInit(): void {
        this.sharedService.userDetails$.pipe(takeUntil(this.unsubscribe$))
            .subscribe(next => {
                console.log('user Id: ', next);
                console.log('user Id: ', this.sharedService.userId);
                this.loadUserDetails(next);
            });
    }

    ngOnDestroy(): void {
        this.unsubscribe$.next();
        this.unsubscribe$.complete();
    }

    loadUserDetails(userId: string): void {
        this.userService.getSingle(userId).subscribe({
            next: (res: NullableUserModel) => {
                this.user = res;
                this.openSideNav();
            },
            error: err => {
                this.user = null;
                alert('Getting user details failed');
            }
        });
    }
    
    openSideNav(): void {
        if (!this.sharedService.isSideNaveOpened) {
            this.sharedService.toggleSideNav();
        }
    }
}
