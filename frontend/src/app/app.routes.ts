import { Routes } from '@angular/router';
import { MovieListComponent } from './features/movies/components/movie-list/movie-list.component';
import { MovieDetailsComponent } from './features/movies/components/movie-details/movie-details.component';
import { ActorListComponent } from './features/actors/components/actor-list/actor-list.component';
import { LoginComponent } from './shared/components/login/login.component';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/movies', pathMatch: 'full' },
  { path: 'movies', component: MovieListComponent },
  { path: 'movies/:id', component: MovieDetailsComponent },
  { path: 'actors', component: ActorListComponent, canActivate: [authGuard] },
  { path: "login", component: LoginComponent },
  { path: '**', redirectTo: '/movies' }
];
