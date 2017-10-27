import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { NgForm } from '@angular/forms';

import { AuthService } from '../../auth.service';

@Component({
    selector: 'signin',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.css']
})
export class SignupComponent {

    error: string = '';
    username: string = '';
    password: string = '';
    password2: string = '';

    constructor(private authService: AuthService) {
        this.authService = authService;
    }

    onSubmit(f: NgForm) {
        this.authService.signup(this.username, this.password, this.password2).subscribe(result => {
            this.authService.authenticate();
            this.error = '';
        }, error => /*this.error = error*/ this.error = 'Unable to create account');
    }

}

