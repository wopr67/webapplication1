import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { SigninComponent } from './components/signin/signin.component';
import { SignoutComponent } from './components/signout/signout.component';
import { SignupComponent } from './components/signup/signup.component';
import { ItemsComponent } from './components/items/items.component';

import { AuthService } from './auth.service';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
		HomeComponent,
		SigninComponent,
        SignoutComponent,
        SignupComponent,
        ItemsComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
			{ path: 'fetch-data', component: FetchDataComponent },
			{ path: 'signin', component: SigninComponent },
            { path: 'signout', component: SignoutComponent },
            { path: 'signup', component: SignupComponent },
            { path: 'items', component: ItemsComponent },
			{ path: '**', redirectTo: 'home' }
        ])
    ]
,
	providers: [
		AuthService
	]
})
export class AppModuleShared {
    constructor() {}
}
