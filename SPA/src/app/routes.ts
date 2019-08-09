import { Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';

export const appRoutes: Routes = [
    // The router works on a 'first match wins' system,
    // Thus its necessary that the wild card route is left at the end.
    { path: 'home', component: HomeComponent },
    { path: 'members', component: MemberListComponent },
    { path: 'messages', component: MessagesComponent },
    { path: 'list', component: ListsComponent },
    { path: '**', pathMatch: 'full', redirectTo: 'home' }
];
