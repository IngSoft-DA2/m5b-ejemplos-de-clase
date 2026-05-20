import { Component, Input, Output, EventEmitter, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Movie } from '../../models/movie.model';

@Component({
  selector: 'app-movie-card',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './movie-card.component.html',
  styleUrls: ['./movie-card.component.css']
})
export class MovieCardComponent {
  @Input() movie!: Movie;
  @Output() movieSelected = new EventEmitter<Movie>();

  ngOnChanges(changes: SimpleChanges): void {
    console.log('MovieCardComponent - ngOnChanges ejecutado');

    if (changes['movie']) {
      const currentMovie = changes['movie'].currentValue;
      const previousMovie = changes['movie'].previousValue;


      if (currentMovie && previousMovie && currentMovie.id !== previousMovie.id) {
        console.log(`     ¡El ID de la película ha cambiado de ${previousMovie.id} a ${currentMovie.id}!`);
      } else if (currentMovie && !previousMovie) {
        console.log('     Primera asignación de la película.');
      }
    } else {
      console.log('  -> Otra propiedad de entrada (no "movie") ha cambiado.');
    }
  }

  ngOnInit(): void {
    console.log('MovieCardComponent - ngOnInit ejecutado: movie is', this.movie.title);
  }

  onMovieClick(): void {
    this.movieSelected.emit(this.movie);
  }
}
