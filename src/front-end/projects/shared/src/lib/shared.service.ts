import { PlatformLocation } from '@angular/common';
import { Injectable } from '@angular/core';
import {Observable, Subject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  private _isProduction: boolean = false;

  public get isProduction(): boolean {
    return this._isProduction;
  }

  public set isProduction(value: boolean) {
    this._isProduction = value;
  }

  private _userId: string | null = null;

  public get userId(): string | null {
    return this._userId;
  }

  public set userId(value: string | null) {
    this._userId = value;
  }
  
  public isSideNaveOpened: boolean = false;
  private sideNav = new Subject<boolean>();
  public sideNav$: Observable<boolean> = this.sideNav.asObservable();

  private userDetails = new Subject<string>();
  public userDetails$: Observable<string> = this.userDetails.asObservable();
  
  constructor(private platformLocation: PlatformLocation) { }

  getServerRoute(): string {
    return this._isProduction ? `${(this.platformLocation as any).location.origin}` : 'https://localhost:7293';
  }
  
  toggleSideNav(): void {
    this.isSideNaveOpened = !this.isSideNaveOpened;
    this.sideNav.next(this.isSideNaveOpened);
  }
  
  loadUserDetails(id: string): void {
    this.userDetails.next(id);
  }

}
