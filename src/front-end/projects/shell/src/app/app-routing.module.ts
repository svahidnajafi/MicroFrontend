import {loadRemoteModule} from '@angular-architects/module-federation';
import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AppComponent} from './app.component';
import {UserListComponent} from './user-list/user-list.component';
import {UserListResolverService} from "./services/user-list.resolver.service";

const routes: Routes = [
    {
        path: '',
        // pathMatch: 'full',
        children: [
            {
                path: '',
                component: UserListComponent,
                resolve: {resolvedData: UserListResolverService},
            },
            {
                path: 'user-details',
                outlet: 'details',
                loadChildren: () =>
                    loadRemoteModule({
                        // type: 'manifest',
                        // remoteName: 'userDetails',
                        // exposedModule: './Module',
                        type: 'module',
                        remoteEntry: 'http://localhost:3000/remoteEntry.js',
                        exposedModule: './Module'
                    }).then((m) => m.UserModule),
            }
        ]
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
