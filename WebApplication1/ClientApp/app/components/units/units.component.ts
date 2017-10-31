import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../../auth.service';

@Component({
    selector: 'units',
    templateUrl: './units.component.html',
    styleUrls: ['./units.component.css']
})
export class UnitsComponent {

    constructor(private authService: AuthService, private router: Router) { }

    ngOnInit() {
        if (!this.authService.isSignedIn)
        {
            this.router.navigate(['signin'], { queryParams: { returnurl:"units"} });
        }
    }

	isSignedIn(){
		return this.authService.isSignedIn;
	}
    userName() {
        return this.authService.userName;
    }
}
