# 📦 Sistema de Gestión de Productos y Órdenes

Sistema integral de gestión de **Productos y Órdenes** desarrollado con **.NET 9** siguiendo principios modernos de **Clean Architecture** combinada con **Domain-Driven Design (DDD)**. La solución proporciona una API REST completa para administrar un catálogo de productos, gestionar inventario y crear/procesar órdenes de compra con validaciones robustas de negocio.

---

## 🏗️ Arquitectura

La solución implementa **Clean Architecture + DDD** organisada en **6 proyectos** con separación clara de responsabilidades:

```
┌─────────────────────────────────────────────────────────���
│                 API (Presentación)                      │
│  - Controllers REST                                     │
│  - Middleware Global                                    │
│  - Swagger/OpenAPI                                      │
│  - Inyección de Dependencias                            │
└─────────────────────────────────────────────────────────┘
                          ↑
┌─────────────────────────────────────────────────────────┐
│            Application (Casos de Uso)                   │
│  - Handlers (Command Pattern)                           │
│  - Interfaces de Casos de Uso                           │
│  - DTOs para transferencia de datos                     │
│  - Commands (órdenes de negocio)                        │
│  - Excepciones de aplicación                            │
└─────────────────────────────────────────────────────────┘
          ↑                              ↑
┌──────────────────────┐      ┌──────────────────────┐
│   Infrastructure     │      │      Domain          │
│   (Persistencia)     │      │   (Lógica Pura)      │
│                      │      │                      │
│ - DbContext          │      │ - Entidades          │
│ - Repositorios       │      │ - Value Objects      │
│ - EF Core            │      │ - Interfaces         │
│ - Configuraciones    │      │ - Agregados          │
│ - Seed de datos      │      │ - Excepciones        │
└──────────────────────┘      └──────────────────────┘
                ↑
┌─────────────────────────────────────────────────────────┐
│          Client (Consumidor/Pruebas)                    │
│  - Cliente HTTP                                         │
│  - Modelos de desérialización                           │
│  - Menú interactivo                                     │
└─────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────┐
│          Api.Tests (Pruebas Unitarias)                  │
│  - Tests de integración                                 │
│  - WebApplicationFactory                                │
└─────────────────────────────────────────────────────────┘
```

---

## 📂 Estructura del Proyecto

```
Orders/
│
├── Domain/                              (Lógica de Negocio)
│   ├── Entities/
│   │   ├── Product.cs
│   │   ├── Order.cs
│   │   └── OrderItem.cs
│   ├── ValueObjects/
│   │   ├── Money.cs
│   │   ├── Quantity.cs
│   │   └── Stock.cs
│   ├── Interfaces/
│   │   ├── IProductRepository.cs
│   │   └── IOrderRepository.cs
│   ├── Exceptions/
│   │   ├── ProductDomainException.cs
│   │   └── OrderDomainException.cs
│   └── Domain.csproj
│
├── Application/                         (Casos de Uso)
│   ├── UseCases/
│   │   ├── Products/
│   │   │   ├── Commands/
│   │   │   │   ├── CreateProductCommand.cs
│   │   │   │   ├── UpdateProductCommand.cs
│   │   │   │   └── DeleteProductCommand.cs
│   │   │   ├── Interfaces/
│   │   │   │   ├── ICreateProductUseCase.cs
│   │   │   │   ├── IUpdateProductUseCase.cs
│   │   │   │   ├── IGetProductsUseCase.cs
│   │   │   │   ├── IGetProductByIdUseCase.cs
│   │   │   │   └── IDeleteProductUseCase.cs
│   │   │   ├── CreateProductHandler.cs
│   │   │   ├── UpdateProductHandler.cs
│   │   │   ├── GetProductsHandler.cs
│   │   │   ├── GetProductByIdHandler.cs
│   │   │   └── DeleteProductHandler.cs
│   │   │
│   │   └── Orders/
│   │       ├── Commands/
│   │       │   ├── CreateOrderCommand.cs
│   │       │   ├── OrderItemCommand.cs
│   │       │   ├── GetOrdersCommand.cs
│   │       │   ├── GetOrderByIdCommand.cs
│   │       │   └── DeleteOrderCommand.cs
│   │       ├── Interfaces/
│   │       │   ├── ICreateOrderUseCase.cs
│   │       │   ├── IGetOrdersUseCase.cs
│   │       │   ├── IGetOrderByIdUseCase.cs
│   │       │   └── IDeleteOrderUseCase.cs
│   │       ├── CreateOrderHandler.cs
│   │       ├── GetOrdersHandler.cs
│   │       ├── GetOrderByIdHandler.cs
│   │       └── DeleteOrderHandler.cs
│   │
│   ├── DTOs/
│   │   ├── CreateProductRequest.cs
│   │   ├── UpdateProductRequest.cs
│   │   ├── ProductResponse.cs
│   │   ├── GetProductsResponse.cs
│   │   ├── CreateOrderRequest.cs
│   │   ├── OrderResponse.cs
│   │   ├── OrderItemResponse.cs
│   │   └── GetOrdersResponse.cs
│   │
│   ├── Common/
│   │   ├── Mappers/
│   │   │   ├── ProductCommandMapper.cs
│   │   │   └── OrderCommandMapper.cs
│   │   ├── ApplicationException.cs
│   │   ├── NotFoundException.cs
│   │   └── ValidationException.cs
│   │
│   └── Application.csproj
│
├── Infrastructure/                      (Persistencia)
│   ├── Persistence/
│   │   ├── ApplicationDbContext.cs
│   │   └── SeedData.cs
│   ├── Repositories/
│   │   ├── ProductRepository.cs
│   │   └── OrderRepository.cs
│   ├── Configurations/
│   │   ├── ProductConfiguration.cs
│   │   ├── OrderConfiguration.cs
│   │   └── OrderItemConfiguration.cs
│   ├── InfrastructureServiceCollectionExtensions.cs
│   └── Infrastructure.csproj
│
├── Api/                                 (Presentación)
│   ├── Controllers/
│   │   ├── ProductsController.cs
│   │   └── OrdersController.cs
│   ├── Extensions/
│   │   └── ApplicationServiceCollectionExtensions.cs
│   ├── Middleware/
│   │   └── GlobalExceptionHandlingMiddleware.cs
│   ├── Program.cs
│   ├── appsettings.json
│   └── Api.csproj
│
├── Api.Tests/                           (Pruebas)
│   ├── CustomWebApplicationFactory.cs
│   ├── ProductsControllerTests.cs
│   ├── OrdersControllerTests.cs
│   └── Api.Tests.csproj
│
├── Client/                              (Consumidor)
│   ├── Models/
│   │   ├── ProductModel.cs
│   │   ├── OrderModel.cs
│   │   ├── OrderItemModel.cs
│   │   ├── GetProductsResponseModel.cs
│   │   └── GetOrdersResponseModel.cs
│   ├── ApiClient.cs
│   ├── Program.cs
│   └── Client.csproj
│
├── Orders.sln
├── README.md
├── .gitignore
└── Orders.http  (o Api.http para pruebas REST)
```

---

## ✨ Características Principales

### 🛍️ Operaciones CRUD Completas

- ✅ **Productos:** Crear, Leer, Actualizar, Eliminar con paginación y filtrado
- ✅ **Órdenes:** Crear, Leer, Listar, Eliminar con restauración automática de stock

### 🔒 Validaciones Robustas de Negocio

- ✅ **Control de stock:** No permite crear órdenes sin inventario suficiente
- ✅ **Congelamiento de precios:** Registra el precio al momento de la orden
- ✅ **Restauración de stock:** Recupera inventario automáticamente al cancelar órdenes
- ✅ **Validaciones de datos:** Nombres, precios, cantidades, etc.
- ✅ **Manejo centralizado de errores:** Respuestas JSON consistentes

### 🔧 Características Técnicas

- ✅ Paginación en listados
- ✅ Filtrado de productos por nombre
- ✅ Logging integrado
- ✅ CORS habilitado
- ✅ Documentación interactiva (Swagger/OpenAPI)
- ✅ Base de datos en memoria para desarrollo rápido
- ✅ Seed de datos iniciales automático
- ✅ 100% Clean Architecture + DDD

---

## 🛠️ Stack Tecnológico

| Componente | Tecnología | Versión |
|-----------|-----------|---------|
| **Framework** | .NET | 9.0 |
| **Lenguaje** | C# | 13 |
| **ORM** | Entity Framework Core | 9.0.12 |
| **Base de Datos** | In-Memory | (desarrollo) |
| **API REST** | ASP.NET Core Web API | 9.0 |
| **Documentación API** | Swagger/OpenAPI | Integrado |
| **Testing** | xUnit/Moq | (preparado) |
| **Patrón de Comunicación** | HTTP/REST | - |

---

## 🎓 Patrones y Principios Implementados

### Patrones de Diseño

- ✅ **Command Pattern:** Cada acción es un comando explícito
- ✅ **Repository Pattern:** Abstracción de acceso a datos
- ✅ **Factory Pattern:** Creación de Value Objects y Agregados
- ✅ **Dependency Injection:** Loose coupling entre capas
- ✅ **Handler Pattern:** Procesamiento de comandos

### Principios SOLID

- ✅ **S:** Responsabilidad única en cada clase
- ✅ **O:** Abierto/Cerrado - extensión sin modificación
- ✅ **L:** Sustitución de Liskov - interfaces consistentes
- ✅ **I:** Segregación de interfaces - interfaces específicas
- ✅ **D:** Inversión de dependencias - depende de abstracciones

### Domain-Driven Design

- ✅ **Agregados:** Product y Order como raíces de agregado
- ✅ **Value Objects:** Money, Quantity, Stock encapsulan validaciones
- ✅ **Ubiquitous Language:** Nombres reflejan el lenguaje del negocio
- ✅ **Entidades:** Con identidad única y ciclo de vida
- ✅ **Excepciones de Dominio:** Validaciones en la capa de negocio

### Clean Architecture

- ✅ **Independencia de Frameworks:** Negocio no depende de tecnología
- ✅ **Testeable:** Cada capa es testeable independientemente
- ✅ **Independencia de UI:** Puede cambiar de REST a gRPC, GraphQL, etc.
- ✅ **Independencia de BD:** Repositorio abstrae la persistencia
- ✅ **Independencia de Frameworks Externos:** Inversión de dependencias

---

## 🚀 Uso Rápido

### Requisitos Previos

- .NET 9.0 SDK o superior
- Visual Studio 2022, Visual Studio Code o similar
- Git

### Instalación

```bash
# 1. Clonar el repositorio
git clone https://github.com/tu-usuario/orders.git
cd orders

# 2. Restaurar dependencias
dotnet restore

# 3. Compilar la solución
dotnet build
```

### Ejecutar la Solución

**Opción 1: Múltiples proyectos simultáneamente (Recomendado)**

```bash
# En Visual Studio
1. Click derecho en la solución
2. Seleccionar "Configure Startup Projects..."
3. Elegir "Multiple startup projects"
4. Seleccionar:
   - Api (Action: Start)
   - Client (Action: Start)
5. Presionar F5 o "Start"
```

**Opción 2: Por terminal (separadas)**

```bash
# Terminal 1: Ejecutar la API
cd Api
dotnet run
# La API estará disponible en: http://localhost:5267

# Terminal 2: Ejecutar el Cliente (en otra terminal)
cd Client
dotnet run
```

### Acceder a Swagger

Una vez que la API esté corriendo:

```
http://localhost:5267/swagger
```

Verás la documentación interactiva de todos los endpoints.

---

## 🔌 Endpoints API

### Productos

| Método | Endpoint | Descripción | Status Esperado |
|--------|----------|-------------|-----------------|
| `GET` | `/api/products` | Obtener productos con paginación | 200 OK |
| `GET` | `/api/products/{id}` | Obtener un producto por ID | 200 OK / 404 |
| `POST` | `/api/products` | Crear un nuevo producto | 201 Created / 400 |
| `PUT` | `/api/products/{id}` | Actualizar un producto | 200 OK / 404 |
| `DELETE` | `/api/products/{id}` | Eliminar un producto | 204 No Content / 404 |

### Órdenes

| Método | Endpoint | Descripción | Status Esperado |
|--------|----------|-------------|-----------------|
| `GET` | `/api/orders` | Obtener órdenes con paginación | 200 OK |
| `GET` | `/api/orders/{id}` | Obtener una orden por ID | 200 OK / 404 |
| `POST` | `/api/orders` | Crear una nueva orden | 201 Created / 400 |
| `DELETE` | `/api/orders/{id}` | Eliminar una orden | 204 No Content / 404 |

---

## 📝 Ejemplos de Uso

### Crear un Producto

**Request:**
```bash
curl -X POST http://localhost:5267/api/products \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Laptop HP ProBook",
    "price": 1299.99,
    "stock": 10
  }'
```

**Response (201 Created):**
```json
{
  "id": 1,
  "name": "Laptop HP ProBook",
  "price": 1299.99,
  "stock": 10,
  "createdAt": "2026-02-24T15:30:00Z"
}
```

### Obtener Productos con Paginación

**Request:**
```bash
curl -X GET "http://localhost:5267/api/products?pageNumber=1&pageSize=10&nameFilter=Laptop" \
  -H "Accept: application/json"
```

**Response (200 OK):**
```json
{
  "products": [
    {
      "id": 1,
      "name": "Laptop HP ProBook",
      "price": 1299.99,
      "stock": 9,
      "createdAt": "2026-02-24T15:30:00Z"
    }
  ],
  "pageNumber": 1,
  "pageSize": 10,
  "totalCount": 1,
  "totalPages": 1
}
```

### Crear una Orden

**Request:**
```bash
curl -X POST http://localhost:5267/api/orders \
  -H "Content-Type: application/json" \
  -d '{
    "customerName": "Juan Pérez",
    "items": [
      {
        "productId": 1,
        "quantity": 2
      }
    ]
  }'
```

**Response (201 Created):**
```json
{
  "id": 1,
  "customerName": "Juan Pérez",
  "items": [
    {
      "id": 1,
      "productId": 1,
      "quantity": 2,
      "unitPrice": 1299.99,
      "subtotal": 2599.98
    }
  ],
  "total": 2599.98,
  "totalItems": 2,
  "createdAt": "2026-02-24T15:35:00Z"
}
```

### Eliminar una Orden (Restaura Stock)

**Request:**
```bash
curl -X DELETE http://localhost:5267/api/orders/1
```

**Response (204 No Content)**
- El stock se restaura automáticamente
- Todos los productos vuelven a su cantidad anterior

---

## 🔒 Validaciones de Negocio Implementadas

### Creación de Orden

```
✓ Validar que el cliente proporciona un nombre
  └─ Si está vacío → 400 Bad Request

✓ Validar que la orden contiene al menos un item
  └─ Si está vacía → 400 Bad Request

✓ Para cada item:
  ├─ Validar que el producto existe
  │  └─ Si no existe → 404 Not Found
  │
  ├─ Validar que hay stock suficiente
  │  └─ Si no hay → 400 Bad Request
  │
  ├─ Congelar el precio actual
  │  └─ Se guarda el precio en ese momento
  │
  └─ Reducir automáticamente el stock
     └─ Se descuenta de inmediato
```

### Eliminación de Orden

```
✓ Validar que la orden existe
  └─ Si no existe → 404 Not Found

✓ Restaurar stock de cada producto
  └─ Se suma automáticamente

✓ Eliminar la orden
  └─ Se elimina de la base de datos
```

---

## 🧪 Ejecutar Pruebas

```bash
# Compilar y ejecutar todas las pruebas
dotnet test

# Ejecutar pruebas de un proyecto específico
dotnet test Api.Tests

# Ejecutar con verbosidad
dotnet test --verbosity detailed

# Ejecutar con cobertura de código (si está configurado)
dotnet test /p:CollectCoverage=true
```

---

## 📦 Compilación y Publicación

```bash
# Compilar en modo Release
dotnet build -c Release

# Publicar para producción
dotnet publish -c Release -o ./publish

# Ejecutar desde publicación
dotnet ./publish/Orders.Api.dll
```

---

## 🔄 Flujo de Datos Ejemplo

```
Cliente (CLI)
    │
    ├─ Opción 1: Ver Productos
    │   └─ GET /api/products → GetProductsHandler → ProductRepository → DB
    │
    ├─ Opción 2: Crear Producto
    │   └─ POST /api/products (DTO)
    │       └─ Mapper convierte a Command
    │           └─ CreateProductHandler (valida en Domain)
    │               └─ Product.Create() (crea Value Objects)
    │                   └─ ProductRepository.Add()
    │                       └─ DbContext.SaveChanges()
    │                           └─ Responde con ProductResponse (DTO)
    │
    └─ Opción 3: Crear Orden
        └─ POST /api/orders (DTO)
            └─ Mapper convierte a Command
                └─ CreateOrderHandler
                    ├─ Valida productos existen
                    ├─ Valida stock suficiente
                    ├─ Congela precios
                    ├��� Reduce stock de productos
                    └─ Guarda orden con items
                        └─ Responde con OrderResponse (DTO)
```

---

## 🎯 Diagrama de Dependencias

```
Client
  ↓
ApiClient (HTTP)
  ↓
╔════════════════════════════════════╗
║          API (Controllers)         ║
║  ├─ ProductsController             ║
║  └─ OrdersController               ║
╚════════════════════════════════════╝
  ↓
╔════════════════════════════════════╗
║      Application (UsesCases)       ║
║  ├─ ICreateProductUseCase          ║
║  ├─ IUpdateProductUseCase          ║
║  ├─ IGetProductsUseCase            ║
║  ├─ IGetProductByIdUseCase         ║
║  ├─ IDeleteProductUseCase          ║
║  ├─ ICreateOrderUseCase            ║
║  ├─ IGetOrdersUseCase              ║
║  ├─ IGetOrderByIdUseCase           ║
║  └─ IDeleteOrderUseCase            ║
║                                    ║
║  Handlers implementan interfaces   ║
║  Commands codifican intenciones    ║
║  DTOs transfieren datos            ║
╚════════════════════════════════════╝
  ↓
╔════════════════════════════════════╗
║     Infrastructure + Domain        ║
║  ├─ ProductRepository              ║
║  ├─ OrderRepository                ║
║  ├─ Product Entity                 ║
║  ├─ Order Entity                   ║
║  ├─ Value Objects (Money, Stock)   ║
║  └─ DbContext                      ║
╚════════════════════════════════════╝
  ↓
Database (In-Memory)
```

---

## 🚨 Manejo de Errores

Todos los errores retornan JSON consistente:

```json
{
  "statusCode": 404,
  "message": "Producto no encontrado.",
  "timestamp": "2026-02-24T15:40:00Z"
}
```

**Códigos de error comunes:**

| Código | Descripción |
|--------|-------------|
| 200 | OK - Solicitud exitosa |
| 201 | Created - Recurso creado |
| 204 | No Content - Eliminado exitosamente |
| 400 | Bad Request - Error de validación |
| 404 | Not Found - Recurso no existe |
| 500 | Internal Server Error - Error del servidor |

---

## 📚 Documentación Adicional

### Convenciones de Código

- **Namespaces:** Reflejan la estructura de carpetas
- **Nombres:** PascalCase para clases/métodos, camelCase para variables
- **Comentarios:** XML para métodos públicos, explicativo donde sea complejo
- **Excepciones:** Específicas por dominio, nunca genéricas

### Próximas Fases (Roadmap)

- ⏳ Tests unitarios exhaustivos
- ⏳ Tests de integración
- ⏳ Autenticación y autorización (JWT)
- ⏳ Logging avanzado (Serilog)
- ⏳ Caché distribuido (Redis)
- ⏳ Validaciones con FluentValidation
- ⏳ Migrations automáticas
- ⏳ Docker support

---

## 🤝 Contribuciones

Las contribuciones son bienvenidas. Para cambios importantes:

1. Fork el proyecto
2. Crea una rama para tu característica (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

---

## 📄 Licencia

Este proyecto está bajo la licencia MIT. Ver el archivo `LICENSE` para más detalles.

---

## ✉️ Contacto

Para preguntas o sugerencias:

- 📧 Abre un issue en el repositorio
- 💬 Discusiones en GitHub

---

## 🎯 Resumen de Logros

Esta solución demuestra:

- ✅ **Implementación correcta de Clean Architecture** con todas sus capas
- ✅ **Domain-Driven Design completo** con agregados, value objects, entities
- ✅ **Principios SOLID** aplicados en todo el código
- ✅ **Patrones de Diseño** (Command, Repository, Factory, Handler, DI)
- ✅ **Validaciones robustas** de reglas de negocio
- ✅ **API REST bien estructurada** y documentada
- ✅ **Manejo centralizado de errores** con middleware global
- ✅ **Código profesional, mantenible y escalable**
- ✅ **Separación clara de responsabilidades** entre capas
- ✅ **Casos de uso del mundo real** (E-commerce)

---

## ▶️ Pasos para ejecutar la solución:

- Establezca múltiples proyectos en la solución
  - Click derecho sobre la solución
  - Seleccione Configure Startup Projects...
  - Elija en el siguiente orden los proyectos:
    - Api
    - Client
- Ejecute la solución desde el nuevo perfil creado, posiblemente aparezca como "New Profile"
- Espere a que carguen tanto el proyecto **Api** como **Client**
- Interactúe con la App de Consola (Client)

**Última actualización:** Febrero 2026  
**Versión:** 1.0.0  
**Estado:** En Desarrollo ✨

