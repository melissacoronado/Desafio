
# Mutants API

Magneto quiere reclutar la mayor cantidad de mutantes para poder luchar contra los X-Men. 

Te ha contratado a ti para que desarrolles un proyecto que detecte si un humano es mutante basándose en su secuencia de ADN. 

## Tabla de Contenidos

- [Instalación](#instalación)
- [Configuración](#configuración)
- [Endpoints](#endpoints)
- [Validaciones](#validaciones)
- [Pruebas](#pruebas)
- [Tecnologías Utilizadas](#tecnologías-utilizadas)

## Instalación

### Requisitos

- [.NET 8.0 o superior](https://dotnet.microsoft.com/download)

### Pasos para ejecutar la API localmente

1. Clona este repositorio:

    ```bash
    git clone https://github.com/melissacoronado/Desafio.git
    ```

2. Navega a la carpeta del proyecto:

    ```bash
    cd ApiMutants\src\ApiMutants.PresentationLayer
    ```

3. Restaura las dependencias:

    ```bash
    dotnet restore
    ```

4. Ejecuta la API:

    ```bash
    dotnet run
    ```

6. La API estará disponible en `http://localhost:5070` o `https://localhost:7034/` para HTTPS.


### Opcional: Ejecutar desde VS Code, instalar pluggin:
	vscode-solution-explorer
	.NET Install Tool
    c#

## Configuración

La API utiliza un archivo `appsettings.json` para su configuración:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "SequenceConfig": {
    "MinQuantity":  4
  }
}
```

Se puede configurar la cantidad minima de secuencias (MinQuantity) para determinar si es un mutante o no.

## Endpoints
POST /mutant
Descripción: Verifica si una secuencia de ADN pertenece a un mutante.

Body:
```json
{
  "dna": ["ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG"]
}
```

Respuesta si es Mutante: HTTP 200 - OK

Respuesta caso No Mutante: HTTP 403 - Bad Request

Respuesta Error para excepciones 500 Internal Server Error


GET /stats
Descripción: Devuelve estadísticas de ADN mutante vs no mutante.

Respuesta exitosa (200 OK):
```json
{
  "count_mutant_dna": 40,
  "count_human_dna": 100,
  "ratio": 0.4
}
```

## Validaciones

El modelo de solicitud MutantsRequest es validado usando FluentValidation. Las validaciones incluyen:

La secuencia de ADN (DNA) no puede estar vacía.
La longitud mínima de cada cadena en el ADN debe ser de 4 caracteres.
```
RuleFor(x => x.DNATable.DNA)
    .NotNull()
    .NotEmpty()
    .WithMessage("La tabla de DNA's no puede estar vacia")
    .Must(x => x.All(sequence => sequence.Length >= 4))
    .WithMessage("La longitud mínima de la secuencia de ADN debe ser 4.");
```

## Pruebas
Las pruebas unitarias se encuentran en el proyecto ApiMutants.Tests y están escritas usando MSTest.

Ejecución de las pruebas:
```bash
dotnet test
```

Ejemplos de pruebas:


```[TestMethod]
public void IsMutant_ShouldThrowException_WhenSmallDNA()
{
    var dna = new List<string>
    {
        "ATG",
        "CAG",
        "TGA"
    };
    var dnaData = new Domain.NonEntities.Mutants { DNA = dna };

    try
    {
        var result = _mutantsService.isMutant(dnaData);
        Assert.Fail("Debio lanzar una excepción");
    }
    catch (Exception ex)
    {
        Assert.AreEqual(ex.Message, "Lista No contiene la cantidad minima de secuencias.");
    }
}
```

## Tecnologías Utilizadas
ASP.NET Core: Framework para construir la API.
FluentValidation: Validación de modelos.
Swagger: Generación automática de documentación de la API.
MediatR: Implementación de CQRS.
MSTest: Framework para pruebas unitarias.

### Documentación de la API
La API utiliza Swagger para la documentación interactiva. Una vez la API esté en ejecución, puedes acceder a la documentación de Swagger en la siguiente URL:

http://localhost:5030/index.html
https://localhost:7039/index.html
