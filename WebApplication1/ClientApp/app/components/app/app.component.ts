import { Component } from '@angular/core';
import { AuthService } from '../../auth.service';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {

    constructor(private authService: AuthService)
    {
        this.authService = authService;
        this.Authenticate();
    }

    private Authenticate() {
        this.authService.authenticate();
    }

}
