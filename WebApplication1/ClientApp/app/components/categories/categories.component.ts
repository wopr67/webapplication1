import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../../auth.service';

@Component({
    selector: 'categories',
    templateUrl: './categories.component.html',
    styleUrls: ['./categories.component.css']
})
export class CategoriesComponent {

    constructor(private authService: AuthService, private router: Router) { }

    ngOnInit() {
        if (!this.authService.isSignedIn)
        {
            this.router.navigate(['signin'], { queryParams: {returnurl:"categories"} });
        }
    }

	isSignedIn(){
		return this.authService.isSignedIn;
	}
    userName() {
        return this.authService.userName;
    }
}
