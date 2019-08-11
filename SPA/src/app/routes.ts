import { Routes } from '@angular/router';

import { AuthGuard } from '@guards/auth.guard';
import { MemberDetailResolver } from '@resolvers/member-detail.resolver.ts';
import { MemberListResolver } from '@resolvers/member-list.resolver';
import { MemberEditResolver } from '@resolvers/member-edit.resolver';

import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';

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
            { path: 'members', component: MemberListComponent, resolve: {usersFromResolver: MemberListResolver} },
            { path: 'members/:id', component: MemberDetailComponent, resolve: {userFromResolver: MemberDetailResolver} },
            { path: 'member/edit', component: MemberEditComponent, resolve: {userFromEditResolver: MemberEditResolver} },
            { path: 'messages', component: MessagesComponent },
            { path: 'list', component: ListsComponent },
            { path: '**', pathMatch: 'full', redirectTo: '' }
        ]
    },
];
