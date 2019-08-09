import { Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [
    // The router works on a 'first match wins' system,
    // Thus its necessary that the wild card route is left at the end.
    // path here and in the wildcard should be empty so if there is an issue with
    // the route it will redirect them to the home page
    { path: '', component: HomeComponent },
    {
        // the path is empty because since
        // this is a first match wins basis, the authguard will activate
        // for any route that is not the home route.
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'members', component: MemberListComponent },
            { path: 'messages', component: MessagesComponent },
            { path: 'list', component: ListsComponent },
            { path: '**', pathMatch: 'full', redirectTo: '' }
        ]
    },
];
