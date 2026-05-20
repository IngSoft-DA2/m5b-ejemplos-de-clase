import { Injectable } from '@angular/core';
import { catchError, map, Observable, of } from 'rxjs';
import { Movie } from '../models/movie.model.js';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MovieService {
  private apiUrl = 'https://api.themoviedb.org/3';
  private apiKey = '36eb10aa56ea823b1b3c7370a05aeb09';

  constructor(private http: HttpClient) { }

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
