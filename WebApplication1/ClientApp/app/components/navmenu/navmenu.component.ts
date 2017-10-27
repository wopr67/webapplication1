import { Component } from '@angular/core';

import { AuthService } from '../../auth.service';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {

    constructor(private authService: AuthService) { }

	isSignedIn(){
		return this.authService.isSignedIn;
	}
    userName() {
        return this.authService.userName;
    }
}
