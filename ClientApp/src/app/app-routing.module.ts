import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {GuestComponent} from "./components/guest/guest.component";
import {GuestGuard} from "./guards/guest.guard";
import {TestsComponent} from "./components/tests/tests.component";
import {AuthorizedUserGuard} from "./guards/authorized-user.guard";
import {TestComponent} from "./components/test/test.component";

const routes: Routes = [
  {path: '', pathMatch: "full", component: GuestComponent, canActivate: [GuestGuard]},
  {path: 'tests', component: TestsComponent, canActivate: [AuthorizedUserGuard]},
  {path: 'tests/:testId', component: TestComponent, canActivate: [AuthorizedUserGuard]},
  {path: '**', redirectTo: '/'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
