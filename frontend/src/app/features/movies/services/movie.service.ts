import { Injectable } from '@angular/core';
import { catchError, map, Observable, of } from 'rxjs';
import { Movie } from '../models/movie.model.js';
import { HttpClient } from '@angular/common/http';

// ── Importamos las variables de entorno ───────────────────────────────────────
// En lugar de hardcodear la URL y la API key aquí, las leemos del archivo
// de entorno. Angular reemplazará este import con environment.prod.ts
// automáticamente cuando hagamos `ng build --configuration production`.
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  // ── Usamos las variables del entorno en lugar de strings literales ─────────
  // Si mañana cambia la URL del API, solo tocamos environment.ts,
  // no tenemos que buscar el string hardcodeado en todos los services.
  private apiUrl = environment.apiUrl;
  private apiKey = environment.apiKey;

  constructor(private http: HttpClient) { }

  // ── Nota sobre el interceptor ──────────────────────────────────────────────
  // Este service NO necesita agregar el header Authorization manualmente.
  // El authInterceptor registrado en app.config.ts lo agrega solo,
  // interceptando esta llamada antes de que llegue al servidor.
  getMovies(): Observable<Movie[]> {
    return this.http.get<any>(`${this.apiUrl}/movie/popular?api_key=${this.apiKey}`).pipe(
      map(response => response.results.map((item: any) => ({
        id: item.id,
        title: item.title,
        releaseYear: new Date(item.release_date).getFullYear(),
        genre: item.genre_ids.join(', '),
        posterUrl: `https://image.tmdb.org/t/p/w500${item.poster_path}`,
        overview: item.overview
      }))),
      catchError(this.handleError<Movie[]>('getMovies', []))
    );
  }

  getMovieById(id: number): Observable<Movie> {
    return this.http.get<any>(`${this.apiUrl}/movie/${id}?api_key=${this.apiKey}`).pipe(
      map(item => ({
        id: item.id,
        title: item.title,
        releaseYear: new Date(item.release_date).getFullYear(),
        genre: item.genres.map((g: any) => g.name).join(', '),
        posterUrl: `https://image.tmdb.org/t/p/w500${item.poster_path}`,
        overview: item.overview
      })),
      catchError(this.handleError<Movie>(`getMovieById id=${id}`))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(`${operation} failed:`, error);
      return of(result as T);
    };
  }
}
