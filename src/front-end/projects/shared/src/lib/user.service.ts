import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http"
import { Observable } from "rxjs";
import { SharedService } from "./shared.service";
import { NullableUserModel, UserModel } from "./user-models";

@Injectable({
    providedIn: 'root'
})
export class UserService {

    constructor(private http: HttpClient, private sharedService: SharedService) {}

    private usersRoute(extension: string = ''): string {
        const baseRoute: string = this.sharedService.getServerRoute();
        if (extension.length > 0)
            return `${baseRoute}/users/${extension}`;
        return `${baseRoute}/users`;
    }

    getByFilter(): Observable<UserModel[]> {
        // Uncomment to use query params
        // const queryParams = new HttpParams()
        //     .set('searchValue', searchValue.toString());
        // return this.http.get<UserModel[]>(this.usersRoute(), { params: queryParams });
        return this.http.get<UserModel[]>(this.usersRoute());
    }

    getSingle(id: string): Observable<NullableUserModel> {
        return this.http.get<NullableUserModel>(this.usersRoute(id));
    }

    upsert(model: any): Observable<string> {
        return this.http.post<string>(this.usersRoute(), model);
    }

    delete(id: string): Observable<string> {
        return this.http.delete<string>(this.usersRoute(id));
    }
}