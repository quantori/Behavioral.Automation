import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegistrationComponent } from './components/registration/registration.component';
import { TermsAndConditionsComponent } from "./components/terms-and-conditions/terms-and-conditions.component";

const routes: Routes = [
  {
    path: 'about-us',
    children: [
      {
        path: 'terms-and-conditions',
        component: TermsAndConditionsComponent,
      },
    ],
  },

  {
    path: 'register',
    component: RegistrationComponent,
  },
  {
    path: '',
    component: RegistrationComponent,
    pathMatch: 'full'
  },
  {
    path: '**',
    redirectTo: '',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
