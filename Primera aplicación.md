# Primera aplicación

### Antes que nada es importante aclarar…

[Standalone vs. NgModules](Primera%20aplicaci%C3%B3n%201f155f234e0b814186f6c5c03d9bafbf/Standalone%20vs%20NgModules%201f155f234e0b80fba1a5cfd33671c527.md)

---

### Una vez instalado todo lo necesario, ejecutaremos nuestra primera aplicación de Angular…

1. Creación del proyecto
    
    <aside>
    💡
    
    `ng new nombre-obligatorio --skip-tests`
    
    *Tengan cuidado porque les puede generar un repo. Tienen que borrar el .git*
    
    </aside>
    

Otras opciones (para que tengan)

```bash
ng new <<nombre de la aplicacion de angular>> --minimal --routing --no-standalone
```

Parámetros:

- `minimal`: sin frameworks de pruebas, solo por temas educativos
- `routing`: genera un modulo para el ruteo de raíz
- `no-standalone`: no genera el componente raíz autogestionable

1. Seleccionar el tipo de estilado. En nuestra caso, seleccionamos `css`...
    
    ![image.png](Primera%20aplicaci%C3%B3n%201f155f234e0b814186f6c5c03d9bafbf/image.png)
    

1. Decimos que no al SSR:
    
    ![image.png](Primera%20aplicaci%C3%B3n%201f155f234e0b814186f6c5c03d9bafbf/image%201.png)
    

1. Ejecutamos la aplicación:
    
    ```bash
    *# Ubicados dentro del directorio...*
    
    ng serve --open
    ```
    
### Ver ejemplo de estructura
[Estructura posible](estructura.md)



# Referencia

- *Material Daniel Acevedo*