import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../auth.service';

@Component({
	selector: 'signin',
    templateUrl: './signin.component.html',
    styleUrls: ['./signin.component.css']
})
export class SigninComponent {

    error: string = '';
    username: string = '';
    password: string = '';
    returnUrl: string = '';
 
    constructor(private authService: AuthService, private route: ActivatedRoute, private router: Router) {
        this.authService = authService;
    }

    ngOnInit() {
        // check for a returnurl in the querystring
        this.route.queryParams.subscribe(params => {
            this.returnUrl = params['returnurl'];
        });
    }

    onSubmit(f: NgForm) {
        this.authService.signin(this.username, this.password).subscribe(result => {
            this.error = '';
            this.authService.authenticate((): void => {
                if (this.returnUrl)
                    this.router.navigate([this.returnUrl]);
            });

        }, error => /*this.error = error*/ this.error = 'Invalid username or password');
    }

}

