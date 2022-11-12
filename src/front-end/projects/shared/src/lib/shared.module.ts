import { NgModule } from '@angular/core';
import { SharedComponent } from './shared.component';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';
import { MatDialogModule } from '@angular/material/dialog';
import { MatRippleModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSidenavModule } from '@angular/material/sidenav';
import {FlexLayoutModule, FlexModule} from "@angular/flex-layout";
import {HttpClientModule} from "@angular/common/http";
import {MatIconModule} from "@angular/material/icon";

const MaterialComponents = [MatButtonModule, MatCardModule, MatInputModule, MatFormFieldModule,
  MatSidenavModule, MatDialogModule, MatListModule, MatRippleModule, MatIconModule]

@NgModule({
  declarations: [
    SharedComponent
  ],
  imports: [
    HttpClientModule,
    MaterialComponents,
    FlexLayoutModule, 
    FlexModule
  ],
  exports: [
    SharedComponent,
    MaterialComponents,
    FlexLayoutModule,
    FlexModule
  ]
})
export class SharedModule { }
