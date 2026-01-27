# Microservicios Hexagonal

## Resumen
Solucion de microservicios en .NET 9 con arquitectura hexagonal, un API Gateway con YARP y OpenAPI expuesto en el puerto 8080. Los servicios usan repositorios en memoria (cache volatil) y publican eventos en un bus configurable (in-memory o RabbitMQ).

## Arquitectura
```
FrontEnd
  |
  v
ApiGateway (YARP + Swagger) :8080
  |-- /users     -> UserService.Api  :5209
  |-- /products  -> ProductService.Api :5174
  |-- /sales     -> SalesService.Api :5005
  |-- /openapi/* -> OpenAPI de cada servicio
  |
  v
Event Bus
  - InMemory (default)
  - RabbitMQ (opcional)
```

Cada servicio sigue capas hexagonales:
- Domain: entidades y contratos
- Application: casos de uso
- Infrastructure: repositorios y adaptadores
- Api: endpoints HTTP (minimal APIs)

## Servicios
### UserService
- Endpoints:
  - `POST /users` crea usuario y publica `UserCreated`
  - `GET /users/{id}` obtiene usuario por id
- Puerto: `5209`
- Persistencia: memoria (se pierde al cerrar)

### ProductService
- Endpoints:
  - `POST /products` crea producto y publica `ProductCreated`
  - `GET /products/{id}` obtiene producto por id
- Puerto: `5174`
- Persistencia: memoria (se pierde al cerrar)

### SalesService
- Endpoints:
  - `POST /sales` registra venta y publica `SaleRegistered`
  - `GET /sales/{id}` obtiene venta por id
- Puerto: `5005`
- Persistencia: memoria (se pierde al cerrar)

### ApiGateway
- Swagger UI: `http://localhost:8080/swagger`
- Agrega OpenAPI de cada microservicio bajo el mismo origen.
- Ruteo por path en `src/ApiGateway/ApiGateway.Api/appsettings.json`.

### NotificationService.Worker
- Suscribe eventos `UserCreated`, `ProductCreated`, `SaleRegistered`.
- Requiere RabbitMQ activo.
- Imprime emails en consola mediante `ConsoleEmailSender`.

## Puertos
- ApiGateway: `8080`
- UserService: `5209`
- ProductService: `5174`
- SalesService: `5005`
- RabbitMQ (opcional): `5672` (AMQP), `15672` (management UI)

## Configuracion de Event Bus
Por defecto se usa in-memory (cache volatil). Para RabbitMQ:

1) Cambiar en cada API:
```
"EventBus": { "UseRabbitMq": true }
```

2) Verificar RabbitMQ en `appsettings.json`:
```
"RabbitMq": {
  "HostName": "localhost",
  "UserName": "guest",
  "Password": "guest",
  "ExchangeName": "microservices.events"
}
```

## Como ejecutar
Desde la raiz:
```
dotnet run --project src\UserService\UserService.Api
dotnet run --project src\ProductService\ProductService.Api
dotnet run --project src\SalesService\SalesService.Api
dotnet run --project src\ApiGateway\ApiGateway.Api
```
Luego abrir `http://localhost:8080/swagger`.

## Carpetas clave
- `src/ApiGateway/ApiGateway.Api`
- `src/UserService`
- `src/ProductService`
- `src/SalesService`
- `src/NotificationService`
- `src/BuildingBlocks`
