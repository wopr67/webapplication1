import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { NgForm } from '@angular/forms';
import { AuthService } from '../../auth.service';

@Component({
    selector: 'signout',
    templateUrl: './signout.component.html'
})
export class SignoutComponent implements OnInit {
    message: string;
    ngOnInit() {
        this.authService.signout();
    }

    constructor(private authService: AuthService) {
        this.authService = authService;
        this.message = "you are signed-out from this site."
    }


}

