import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router'; // Para acceder a los parámetros de ruta y routerLink
import { switchMap } from 'rxjs/operators';
import { Movie } from '../../models/movie.model';
import { MovieService } from '../../services/movie.service';

@Component({
  selector: 'app-movie-details',
  imports: [CommonModule, RouterModule],
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {
  movie: Movie | undefined;
  isLoading: boolean = true;
  error: string | null = null;

  constructor(
    private route: ActivatedRoute, // Para obtener parámetros de la URL
    private movieService: MovieService
  ) { }

  ngOnInit(): void {
    this.route.paramMap.pipe(
      switchMap(params => {
        const movieId = Number(params.get('id'));
        if (isNaN(movieId) || movieId === 0) {
          this.error = 'ID de película inválido.';
          this.isLoading = false;
          return []; // Retorna un Observable vacío para no continuar
        }
        this.isLoading = true;
        this.error = null;
        return this.movieService.getMovieById(movieId);
      })
    ).subscribe({
      next: (data) => {
        this.movie = data;
        this.isLoading = false;
        if (!data) {
          this.error = 'Película no encontrada.';
        }
      },
      error: (err) => {
        console.error('Error fetching movie details:', err);
        this.error = 'No se pudieron cargar los detalles de la película. Inténtelo de nuevo más tarde.';
        this.isLoading = false;
      }
    });
  }
}
