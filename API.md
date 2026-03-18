# APIs, REST y diseño de Web APIs

## Qué es una API y para qué sirve?

Una API (Application Programming Interface) es una interfaz: un conjunto de reglas, definiciones y formatos que permiten que dos componentes de softwares se hablen sin que uno tenga que conocer cómo está implementado el otro.

Se podria decir que es como un contrato: si un cliente envía una solicitud con cierta estructura, el servidor responde de forma predecible.

En la práctica, una API habilita:

- Integración entre sistemas: internos o de terceros.
- Separación de responsabilidades: backend y frontend evolucionan de manera más independiente.
- Reutilización: múltiples clientes consumen el mismo backend: web, mobile, integraciones.
- Escalabilidad: distintos equipos pueden trabajar sobre distintos servicios y si identificamos que un componente requiere mas recursos podemos darselos de manera aislada.

## Qué es una Web API?

Una Web API es una API expuesta a través de la web usando HTTP/HTTPS. Un cliente (browser, app mobile, otro backend, etc.) se comunica con un servidor enviando requests HTTP y recibiendo responses HTTP.

REST (Representational State Transfer) es un estilo arquitectónico. No es una tecnología ni un framework: son restricciones y principios para diseñar servicios web que tienden a ser más escalables, mantenibles y simples de consumir. Cuando esa Web API respeta los principios REST, solemos decir que es  una API RESTful.

En REST trabajamos con:

- Recursos, identificados por URIs.
- Representaciones de esos recursos, que para el curso será JSON.
- Operaciones sobre recursos usando métodos HTTP (GET/POST/PUT/DELETE, etc.).
- Mensajes autodescriptivos.

## Principios de un sistema REST

REST propone algunas restricciones que no siempre se aplican “perfectas”, pero cuanto más nos acerquemos, mejor se comporta el sistema a largo plazo.

### 1- Interfaz uniforme

La interfaz es el contrato. Para sostenerlo:

- Identificamos recursos con URIs.
- La representación debe traer información suficiente para operar.
- Los mensajes son autodescriptivos (content-type, status code, etc.).

### 2- Sin estado (stateless)

Cada request tiene que incluir toda la información necesaria para procesarla. El servidor no guarda “contexto de sesión” entre requests. En casos en los que hay autenticación/autorización, la credencial viaja en cada request.

### 3- Cliente / servidor

Cliente y servidor se desacoplan. Y tienen responsabilidades claras con el objetivo de que ambos respeten el contrato y con la idea de que cada parte puede evolucionar por separado.

### 4- Cacheable

Las responses pueden indicar si son cacheables y por cuánto tiempo. Bien usado reduce latencia y carga del servidor.

### 5-  Sistema por capas (layered / tiered)

El cliente no tiene por qué saber si está hablando con el servidor final o con intermediarios (gateway, proxy, balanceador, CDN). Esto habilita capas de seguridad, balanceo y caché compartido.

## Cómo se ve una request/response HTTP en una Web API

Request y Response son los dos mensajes básicos de una comunicación HTTP en el modelo cliente/servidor. Mientras que el Request es el ensaje que envía el cliente (frontend, Postman, app mobile, otro backend) al servidor para pedir una acción sobre un recurso. El response es el mensaje que devuelve el servidor como resultado de procesar la request.

### Componentes de una request

- **URL/URI**: identifica el recurso.
- **Método HTTP**: define la acción.
- **Headers**: metadatos (autenticación, content-type, caching, etc.).
- **Query params**: filtros, paginación, selección de campos, etc.
- **Body**: datos de entrada (usualmente en POST/PUT/PATCH).

### Componentes de una response

- **Status code**: resultado (éxito o error).
- **Headers**: metadatos (content-type, caching, etc.).
- **Body**: representación del recurso o detalle del resultado/error.

## Diseño de recursos (endpoints)

### Qué es un endpoint (punto final)?

Un endpoint (o punto final) es el “lugar” donde una API expone una funcionalidad. En Web APIs, se entiende como la combinación de:

- una **ruta** (por ejemplo `/users` o `/users/{id}`)
- un **método HTTP** (GET/POST/PUT/DELETE, etc.)

Por ejemplo, `GET /users/10` y `DELETE /users/10` apuntan al mismo recurso pero son endpoints distintos, porque el método cambia la intención.

### Qué es un recurso?

Un recurso es “algo” del dominio que queremos exponer (por ejemplo: usuarios, productos, órdenes). Se identifica con una URI estable.

Ejemplos típicos:

- `/users`
- `/products`
- `/orders`
- `/owners/{ownerId}/dogs`

#### Reglas prácticas para URIs

- **Sustantivos, no verbos**: `/users` en vez de `/getUsers`.
- **Plural consistente**: `/users` y `/orders`, sin mezclar singular/plural.
- **Minúsculas**: evita sorpresas en URLs.
- **Nombres concretos**: mejor `/admins` que `/things`.
- **Relaciones claras**: `/owners/{id}/dogs` cuando aporta.
- **Complejidad por query params**: `/dogs?leashed=true`.
- **No más de 2–3 niveles** salvo que el dominio realmente lo pida.

#### Acciones que no son recursos

Algunas funcionalidades no “son un recurso” (por ejemplo, una conversión o cálculo). En esos casos es aceptable modelar una acción como endpoint:

```
/convert?from=EUR&to=CNY&amount=100
```

Clave: documentar bien parámetros, validaciones y formato de respuesta, porque no se entiende solo mirando la URI.

## Métodos HTTP y semántica (CRUD)

HTTP define un estándar de comunicación entre cliente y servidor con la finalidad de hablar "el mismo idioma” para que cualquier sistema pueda integrarse con otro. Con los métodos HTTP expresamos la intención de la operación sobre un recurso. Respetando la semántica y usando  status codes correctos, la API se vuelve predecible, fácil de consumir y más mantenible.

| Método | Intención típica             | Ejemplo               | Idempotente                                       |
| ------- | ------------------------------ | --------------------- | ------------------------------------------------- |
| GET     | Leer                           | `GET /dogs`         | Sí                                               |
| POST    | Crear                          | `POST /dogs`        | No                                                |
| PUT     | Reemplazar/actualizar completo | `PUT /dogs/{id}`    | Sí (siempre que el body sea el “estado final”) |
| DELETE  | Eliminar                       | `DELETE /dogs/{id}` | Sí (el estado final es “no existe”)            |

idempotente significa que aplicar la misma request N veces deja el sistema en el mismo estado final. La respuesta puede variar (por ejemplo, `DELETE` puede devolver `204` y luego `404`), pero el estado final no cambia.

## Formato de datos: JSON, serialización y convenciones

En Web APIs modernas, el formato por defecto suele ser **JSON** (aunque algunas APIs soportan también XML u otros formatos según el caso).

- **Serialización**: convertir un objeto en memoria a JSON (response).
- **Deserialización**: convertir JSON a un objeto/estructura en memoria (request).

Convenciones recomendadas para JSON:

- Usar `camelCase` en propiedades.
- Mantener consistencia de nombres (no mezclar `snake_case`, `PascalCase`, etc.).
- Evitar exponer directamente modelos de persistencia si no corresponde.

### Qué es la especificación formal de una API?

La especificación de una API es la descripción formal del contrato que el backend ofrece para que otros lo consuman sin conocer su implementación. Normalmente define:

- endpoints disponibles (rutas + métodos)
- estructura de requests (headers, query params, body)
- estructura de responses (status codes, body, ejemplos)
- formato de errores y validaciones
- autenticación/autorización (cómo “me identifico”)

En la práctica, esto suele representarse con estándares/herramientas como **OpenAPI (Swagger)**, que permiten documentar y probar la API de forma consistente.

#### Ejemplo de especificación (según el controller MoviesController del ejemplo del repositorio)

Recurso principal: `Movie`
Base URL: `/api/movies`

| Recurso | Acción | Ubicacion            | Headers           | Paràmetros | Modelo de entrada | Modelo de Salida | Respuestas                                      | Justificación del recurso    |
| ------- | ------- | -------------------- | ----------------- | ----------- | ----------------- | ---------------- | ----------------------------------------------- | ----------------------------- |
| Movies  | GET     | `/api/movies`      | content-type-json | N.A.        | N.A.              | [MoviesDTO]      | 200 Ok, 404 NotFound, 500 Intenal Error         | Obtener todas las películas  |
| Movies  | GET     | `/api/movies/{id}` | content-type-json | id: int     | N.A.              | MoviesDTO        | 200 Ok, 404 NotFound, 500 Intenal Error         | Obtener una película por id  |
| Movies  | POST    | `/api/movies`      | content-type-json | N.A.        | MoviesDTO         | MoviesDTO        | 201 Created, 404 NotFound, 500 Intenal Error    | Atualizar título y estrellas |
| Movies  | PUT     | `/api/movies/{id}` | content-type-json | id:int      | MoviesDTO         | N.A.             | 204 No Content, 404 NotFound, 500 Intenal Error | Crear una película           |
| Movies  | PUT     | `/api/movies/{id}` | content-type-json | id:int      | N.A.              | N.A.             | 204 No Content, 404 NotFound, 500 Intenal Error | Eliminar una película        |

Esquema del recurso `Movie` (JSON):

| Campo     | Tipo            |    Requerido | Ejemplo       |
| --------- | --------------- | -----------: | ------------- |
| `id`    | number (int)    | No (en POST) | `1`         |
| `title` | string          |          Sí | `"Hoppers"` |
| `stars` | number (double) |           No | `4.3`       |

## Data binding / model binding en Web APIs (ASP.NET Core)

En el contexto de una Web API, cuando hablamos de “data binding” normalmente nos referimos a model binding: el mecanismo que toma datos de una request HTTP y los “bindea” (mapea) a parámetros y objetos que recibe el endpoint.

### 8.2.1 Fuentes de datos típicas (binding sources)

En una request podés recibir datos desde:

- **Route params**: parte de la URL (por ejemplo `/dogs/10`).
- **Query params**: `?limit=25&offset=50`.
- **Headers**: metadatos (por ejemplo `Authorization`, `If-None-Match`, etc.).
- **Body**: normalmente JSON para crear/actualizar recursos.

En ASP.NET Core esto se ve en endpoints con parámetros anotados (o inferidos):

```csharp
[HttpGet("/dogs/{id}")]
public IActionResult GetById([FromRoute] int id) => Ok();

[HttpGet("/dogs")]
public IActionResult GetAll([FromQuery] int limit = 25, [FromQuery] int offset = 0) => Ok();

[HttpPost("/dogs")]
public IActionResult Create([FromBody] CreateDogRequest request) => Created("", null);
```

La idea es que el contrato quede claro: qué parte de la request alimenta cada dato.

## Manejo de errores: que sea útil y seguro

En una API, del lado del cliente “todo es caja negra”. Si los errores están mal diseñados, se vuelve imposible diagnosticar y corregir.

Un buen diseño de errores logra:

- Mensajes interpretables por personas y para quién consume la API.
- Señales claras para sistemas ya que nos permite  automatizar decisiones.
- Menos soporte y menos “debug a ciegas”.

### Status codes

No hace falta usar los +70 status codes. Conviene un set chico, conocido y consistente, por ejemplo:

- `200 OK`
- `201 Created`
- `204 No Content`
- `400 Bad Request`
- `401 Unauthorized`
- `403 Forbidden`
- `404 Not Found`
- `409 Conflict`
- `500 Internal Server Error`

Referencia: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes

## Versionado de la API

El versionado protege a los clientes frente a cambios incompatibles.

Formas comunes de versionar:

1. **En la URI**: `/v1/users`, `/v2/users`
2. **Por query param**: `/users?version=1`
3. **Por header**: `X-Api-Version: 1`

Buenas prácticas recomendadas:

- No publicar sin versión (que sea obligatoria).
- Prefijar con `v` y ubicarla lo más a la izquierda posible: `/v1/...`
- Usar números simples (evitar `v1.2` si no hay una estrategia fuerte detrás).

## Lecturas recomendadas

- Diseño de API (ebook): https://aulas.ort.edu.uy/pluginfile.php/441401/mod_resource/content/1/api-design-ebook-2012-03.pdf
