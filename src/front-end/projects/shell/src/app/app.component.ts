import { Component } from '@angular/core';
import { SharedService } from 'shared';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(public sharedService: SharedService) {
  }
  
  title = 'shell';

  closeSideNav(): void {
    this.sharedService.toggleSideNav();
  }
}
