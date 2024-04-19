import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Component Imports
import { FeedbackComponent } from './components/feedback/feedback.component'
import { FeedbackListComponent } from './components/feedback-list/feedback-list.component'
import { HomeComponent } from './components/home/home.component';
import { QuestionSetAddComponent } from './components/questionset-add/questionset-add.component';
import { QuestionSetsComponent } from './components/questionsets/questionsets.component';
import { SurveyAddComponent } from './components/survey-add/surveys-add.component';
import { SurveysComponent } from './components/surveys/surveys.component';
 
// Auth
import { AuthGuard } from './auth/auth.guard';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { PasswordComponent } from './components/password/password.component';
import { FeedbackLandingComponent } from './components/feedback-landing/feedback-landing.component';

const routes: Routes = [
    { path: '', component: HomeComponent, pathMatch: 'full' },
    { 
      path: 'surveys', 
      component: SurveysComponent,
      data: { title: 'Surveys' },
      canActivate: [AuthGuard]
    },
    {
      path: 'surveys-add',
      component: SurveyAddComponent,
      data: { title: 'Add Survey' },
      canActivate: [AuthGuard]
    },
    {
      path: 'questionsets',
      component: QuestionSetsComponent,
      data: { title: 'QuestionSets' },
      canActivate: [AuthGuard]
    },
    {
      path: 'questionsets-add',
      component: QuestionSetAddComponent,
      data: { title: 'Add Survey' },
      canActivate: [AuthGuard]
    },
    {
      path: 'feedback',
      component: FeedbackListComponent,
      data: { title: 'My Feedback' },
      canActivate: [AuthGuard]
    },
    {
      path: 'feedback/:surveyId',
      component: FeedbackComponent,
      data: { title: 'Survey Feedback' }
    },
    {
      path: 'feedback-landing',
      component: FeedbackLandingComponent,
      data: { title: 'Thank you' }
    },
    {
      path: 'login',
      component: LoginComponent,
      data: { title: 'Login' }
    },
    {
      path: 'register',
      component: RegisterComponent,
      data: { title: 'Register' },
      canActivate: [AuthGuard]
    },
    {
      path: 'password',
      component: PasswordComponent,
      data: { title: 'Password' },
      canActivate: [AuthGuard]
    },
    {
      path: '**',
      redirectTo: ''
    }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}