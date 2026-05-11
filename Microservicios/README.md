# Microservicios en .NET 10: HTTP publico + RabbitMQ interno

Este repo ahora replica un patron mas cercano al proyecto NestJS que estabas comparando:

- `ApiGateway` expone la API HTTP publica
- `ms-user` y `ms-contact` ya no exponen endpoints REST al cliente
- la comunicacion interna entre servicios se hace con `RabbitMQ`
- `AuditService` consume eventos asincronos para mostrar publish/subscribe

## Arquitectura

```text
Cliente HTTP
  -> ApiGateway :7000
      -> request/reply por RabbitMQ
          -> ms-user
              -> users_db
          -> ms-contact
              -> contacts_db
      -> GET /api/user-contact/{userId}
          -> consulta UserService por mensaje
          -> consulta ContactService por mensaje
          -> compone la respuesta

Eventos asincronos
  -> UserService publica UserCreatedEvent
  -> ContactService publica ContactCreatedEvent
  -> AuditService los consume
```

## Que cambio frente a la version anterior

Antes:

- `ApiGateway` hacia proxy HTTP
- `UserService` y `ContactService` tenian controllers HTTP propios
- habia Swagger en los tres proyectos

Ahora:

- solo `ApiGateway` expone HTTP publico
- `ms-user` y `ms-contact` reciben mensajes RabbitMQ
- el contrato interno vive en `src/shared-contracts`
- Swagger solo tiene sentido en el Gateway, porque Swagger documenta HTTP

## Proyectos

```text
src/
  ApiGateway/
    Controllers/
      UsersController.cs
      ContactsController.cs
      UserContactController.cs
    Dtos/
    Services/
    Program.cs

  Contracts/
    Users/UserContracts.cs
    Contacts/ContactContracts.cs

  UserService/
    Consumers/
      CreateUserConsumer.cs
      GetUserByIdConsumer.cs
    Data/
    Dtos/
    Entities/
    Repositories/
    Services/
    Program.cs

  ContactService/
    Consumers/
      CreateContactConsumer.cs
      GetContactsByUserIdConsumer.cs
    Data/
    Dtos/
    Entities/
    Repositories/
    Services/
    Program.cs

  AuditService/
    Consumers/
      UserCreatedAuditConsumer.cs
      ContactCreatedAuditConsumer.cs
    Program.cs
```

## Patrones de comunicacion

### 1. HTTP publico

El cliente solo conoce estas rutas:

- `POST /api/users`
- `GET /api/users/{id}`
- `POST /api/contacts`
- `GET /api/contacts/user/{userId}`
- `GET /api/user-contact/{userId}`

Estas rutas viven en `ApiGateway`.

### 2. Request/Reply por RabbitMQ

Cuando el Gateway necesita crear o consultar datos, envia un mensaje a RabbitMQ y espera respuesta.

Ejemplos:

- `CreateUserCommand`
- `GetUserByIdQuery`
- `CreateContactCommand`
- `GetContactsByUserIdQuery`

En NestJS eso se parece a:

```ts
client.send(PATTERN, payload)
```

En este ejemplo .NET se hace con `MassTransit` y `IRequestClient<T>`.

### 3. Publish/Subscribe por eventos

Despues de crear un usuario o un contacto, los microservicios publican eventos:

- `UserCreatedEvent`
- `ContactCreatedEvent`

`AuditService` los consume sin que el Gateway tenga que esperar.

Eso te deja ver dos estilos dentro del mismo repo:

- `request/reply` para operaciones que necesitan respuesta inmediata
- `publish/subscribe` para auditoria y procesos desacoplados

## Equivalencia con NestJS

NestJS actual:

- `@Controller()` en el gateway expone HTTP
- `ClientProxy.send()` envia mensajes
- `@MessagePattern()` recibe mensajes en el microservicio
- eventos pueden publicarse por broker

.NET en este repo:

- `ControllerBase` en `ApiGateway` expone HTTP
- `IRequestClient<T>` envia comandos y queries
- `IConsumer<T>` recibe mensajes en el microservicio
- `context.Publish(...)` publica eventos

## Flujo completo

### Crear usuario

1. El cliente llama `POST /api/users` en el Gateway
2. `ApiGateway` crea `CreateUserCommand`
3. RabbitMQ entrega el mensaje a `UserService`
4. `CreateUserConsumer` usa `IUserService`
5. `UserService` guarda en `users_db`
6. `UserService` responde con `UserResult`
7. `UserService` publica `UserCreatedEvent`
8. `AuditService` consume el evento y lo registra

### Crear contacto

1. El cliente llama `POST /api/contacts`
2. El Gateway envia `CreateContactCommand`
3. `ContactService` guarda en `contacts_db`
4. responde con `ContactItem`
5. publica `ContactCreatedEvent`
6. `AuditService` lo consume

### Endpoint agregador

1. El cliente llama `GET /api/user-contact/{userId}`
2. El Gateway envia `GetUserByIdQuery`
3. En paralelo envia `GetContactsByUserIdQuery`
4. espera ambas respuestas
5. construye:

```json
{
  "userId": "...",
  "fullName": "Nombre Apellido",
  "email": "usuario@test.com",
  "contacts": [
    {
      "userContactKey": "USR-{userId}-CNT-{contactId}",
      "contactId": "...",
      "contactName": "Juan Perez",
      "phone": "0999999999",
      "email": "juan@test.com"
    }
  ]
}
```

## Ejecutar en local

```bash
dotnet run --project src/ms-user/MsUser.csproj --launch-profile http
dotnet run --project src/ms-contact/MsContact.csproj --launch-profile http
dotnet run --project src/ms-audit/MsAudit.csproj --launch-profile AuditService
dotnet run --project src/ms-blockchain/MsBlockchain.csproj --launch-profile BlockchainService
dotnet run --project src/api-gateway/ApiGateway.csproj --launch-profile http
```

Servicios locales:

- `ApiGateway`: `http://localhost:7000`
- `Swagger`: `http://localhost:7000/swagger`
- `RabbitMQ AMQP`: `localhost:5672`
- `RabbitMQ Management`: `http://localhost:15672`
- `PostgreSQL`: `localhost:5432`

Usuario RabbitMQ:

- `guest`
- `guest`

Usuario PostgreSQL:

- `postgres`
- `admin`

## Probar desde Postman

Crear usuario:

```http
POST http://localhost:7000/api/users
Content-Type: application/json

{
  "documentNumber": "1234567890",
  "firstName": "Ana",
  "lastName": "Gomez",
  "email": "ana@test.com"
}
```

Consultar usuario:

```http
GET http://localhost:7000/api/users/{userId}
```

Crear contacto:

```http
POST http://localhost:7000/api/contacts
Content-Type: application/json

{
  "userId": "{userId}",
  "contactName": "Juan Perez",
  "phone": "0999999999",
  "email": "juan@test.com"
}
```

Consultar contactos:

```http
GET http://localhost:7000/api/contacts/user/{userId}
```

Consultar agregado:

```http
GET http://localhost:7000/api/user-contact/{userId}
```

## Cosas importantes para aprender

- el `Gateway` es el contrato publico HTTP
- los microservicios internos ya no se acoplan por URL
- `Contracts` cumple el papel del contrato de mensajes compartido
- `ms-user` y `ms-contact` siguen siendo duenos de su base
- `AuditService` muestra un consumidor desacoplado por eventos

## Comandos base usados

```bash
dotnet new sln -n RabbitMqGatewayDemo
dotnet new webapi -n ApiGateway -o src/api-gateway
dotnet new webapi -n MsUser -o src/ms-user --use-controllers
dotnet new webapi -n MsContact -o src/ms-contact --use-controllers
dotnet new classlib -n SharedContracts -o src/shared-contracts
dotnet new worker -n MsAudit -o src/ms-audit
```

## Diferencia conceptual con la version HTTP anterior

Version anterior:

- mas simple
- ideal para aprender microservicios basicos
- comunicacion interna por HTTP
- Swagger por microservicio

Version actual:

- mas parecida a una arquitectura empresarial
- contrato publico solo en el Gateway
- request/reply interno por RabbitMQ
- eventos asincronos separados
- mejor ejemplo para comparar con el proyecto NestJS que usa gateway + broker
