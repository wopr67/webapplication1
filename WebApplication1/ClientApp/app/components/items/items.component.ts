import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../../auth.service';

@Component({
    selector: 'items',
    templateUrl: './items.component.html',
    styleUrls: ['./items.component.css']
})
export class ItemsComponent {

    constructor(private authService: AuthService, private router: Router) { }

    ngOnInit() {
        if (!this.authService.isSignedIn)
        {
            this.router.navigate(['signin'], { queryParams: {returnurl:"items"} });
        }
    }

	isSignedIn(){
		return this.authService.isSignedIn;
	}
    userName() {
        return this.authService.userName;
    }
}
