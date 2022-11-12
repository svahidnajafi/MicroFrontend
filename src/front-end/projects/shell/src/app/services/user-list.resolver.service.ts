import {Injectable} from "@angular/core";
import {catchError, Observable, of} from "rxjs";
import {ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot} from "@angular/router";
import {UserModel, UserService} from "shared";

@Injectable({
    providedIn: 'root'
})
export class UserListResolverService implements Resolve<Observable<UserModel[]>> {
    
    constructor(private service: UserService, private router: Router) {}
    
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<UserModel[]> {
        return this.service.getByFilter().pipe(
            catchError(err => {
                this.router.navigate(['not-found']);
                return of(err);
            })
        );
    }
    
}