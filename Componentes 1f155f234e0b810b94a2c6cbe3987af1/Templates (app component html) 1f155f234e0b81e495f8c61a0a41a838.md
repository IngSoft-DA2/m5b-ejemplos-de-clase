# Templates (app.component.html)

# Interpolación

*Inserto valores de variables o expresiones (definidas en el app.component.ts) en el template*

```html
<h1>{{ title }}</h1>
```

# Condicionales e iteraciones…

<aside>
📌

Cuidado… esto es a partir de v17+. En versiones anteriores se trabaja con *directives…*

</aside>

### En v17+….

```html
<!-- app.component.html -->

@if (isLoggedIn) {
	<p>Welcome!</p>
} @else {
	<p> Please log in... </p>
}

@switch (expression) {
  @case (value1) {
    <!-- Content 1 -->
  }
  @case (value2) {
    <!-- Content 2 -->
  }
  @default {
    <!-- Content 3 -->
  }
}

@for (movie of movies; track movie.id) {
  <li>{{ movie.name }}</li>
} @empty {
	<p> Empty cart </p>
}
```

### Sino… con *directives*…

```html
<!-- app.component.html -->

<div *ngIf="isLoggedIn; else loginBlock">
  <p>Welcome!</p>
</div>

<ng-template #loginBlock>
  <p>Please log in...</p>
</ng-template>

<ul *ngFor="let movie of movies;">
  <li>{{ movie.name }}</li>
</ul>
```