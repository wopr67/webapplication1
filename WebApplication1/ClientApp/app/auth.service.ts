import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map'

@Injectable()
export class AuthService{

    public isSignedIn: boolean;
    public userName: string;
    private http: Http;
    private baseUrl: string;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string)
    {
        this.http = http;
        this.baseUrl = baseUrl;
    }

    public signin(u: string, p: string) {
        var params = { "username": u, "password": p };
        return this.http.post(this.baseUrl + 'api/Account/Signin', params);
    }

    public signup(u: string, p: string, p2 : string) {
        var params = { "username": u, "password": p, "password2": p2 };
        return this.http.post(this.baseUrl + 'api/Account/Signup', params);
    }

    public signout() {
        this.http.get(this.baseUrl + 'api/Account/SignOut').subscribe(result => {
            this.authenticate();
        }, error => console.error(error));
    }

    public authenticate() {
        this.http.get(this.baseUrl + 'api/Account/Authenticate')
            .map(res => res.json())
            .subscribe(result => {
                this.isSignedIn = result.isAuthenticated;
                this.userName = result.username;
            }, error => console.error(error));
    }

}
