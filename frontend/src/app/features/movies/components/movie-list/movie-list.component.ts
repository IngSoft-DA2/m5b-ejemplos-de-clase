import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MovieCardComponent } from '../movie-card/movie-card.component';
import { MovieService } from '../../services/movie.service.js';
import { Movie } from '../../models/movie.model.js';

@Component({
  selector: 'app-movie-list',
  standalone: true,
  imports: [CommonModule, RouterModule, MovieCardComponent],
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css']
})
export class MovieListComponent implements OnInit {
  movies: Movie[] = [];
  isLoading: boolean = true;
  error: string | null = null;
  errorClass : string = "error-message";

  constructor(private movieService: MovieService) { }

  ngOnInit(): void {
    this.loadMovies();
  }

  loadMovies(): void {
    this.isLoading = true;
    this.error = null;
    this.movieService.getMovies().subscribe({
      next: (data) => {
        this.movies = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error fetching movies:', err);
        this.error = 'No se pudieron cargar las películas. Inténtelo de nuevo más tarde.';
        this.isLoading = false;
      }
    });
  }
}
