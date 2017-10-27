import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { NgForm } from '@angular/forms';

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
    
    constructor(private authService: AuthService) {
        this.authService = authService;
    }

    onSubmit(f: NgForm) {
        this.authService.signin(this.username, this.password).subscribe(result => {
            this.authService.authenticate();
            this.error = '';
        }, error => /*this.error = error*/ this.error = 'Invalid username or password');
	  }

}

