import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { authGuard } from './_guards/auth.guard';
import { TestErrorComponent } from './errors/test-error/test-error.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MembersEditComponent } from './members-edit/members-edit.component';
import { preventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';
// default - first matched route wins
const routes: Routes = [
  {path:'',component:HomeComponent},
  // applying authguard for single route
  // {path:'members',component:MemberListComponent,canActivate:[authGuard]},
  // applying authguard for multiple child routes
  {path:'',
  runGuardsAndResolvers:'always',
  canActivate:[authGuard],
  children:[
    {path:'members',component:MemberListComponent},
    {path:'members/:username',component:MemberDetailComponent},
    {path:'member/edit',component:MembersEditComponent,canDeactivate:[preventUnsavedChangesGuard]},
    {path:'lists',component:ListsComponent},
    {path:'messages',component:MessagesComponent},
  ]
},
{path:'errors',component:TestErrorComponent},
{path:'not-found',component:NotFoundComponent},
{path:'server-error',component:ServerErrorComponent},
// Wildcard component - redirects to below when route not present in above list
  {path:'**',component:NotFoundComponent,pathMatch:'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }