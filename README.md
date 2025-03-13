### **📌 README.md**
```markdown
# 📌 PrestamosService - Backend

Backend desarrollado en **ASP.NET Core** para la gestión de préstamos y generación de planes de pago.

## 🚀 Características
- CRUD de préstamos
- Generación y eliminación de planes de pagos
- API RESTful
- Uso de **Entity Framework Core** y **SQL Server**

---

## 📦 **Requisitos Previos**
Antes de ejecutar el proyecto, asegúrate de tener instalado:

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [SQL Server](https://www.microsoft.com/es-es/sql-server/sql-server-downloads) (o usar **SQL Server Express**)
- [Visual Studio 2022](https://visualstudio.microsoft.com/es/) (Recomendado)
- [Postman](https://www.postman.com/) (Opcional, para probar la API)

---

## ⚙️ **Configuración del Proyecto**
### 1️⃣ Clonar el Repositorio
```bash
git clone https://github.com/dervinluna/BackendPruebaTecnica.git
cd BackendPruebaTecnica
```

### 2️⃣ Configurar la Base de Datos
El proyecto usa **SQL Server**, y la cadena de conexión está en el archivo `appsettings.json`. Asegúrate de modificarla según tu entorno:

📌 **Ubicación:** `appsettings.json`
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=PrestamosDB;User Id=sa;Password=tu_contraseña;"
}
```
Si usas **SQL Server Express**, cambia la cadena de conexión a:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=PrestamosDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### 3️⃣ Aplicar Migraciones y Crear la Base de Datos
Ejecuta los siguientes comandos en la terminal de Visual Studio:

```bash
dotnet ef database update
```

Si necesitas generar las migraciones manualmente, usa:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## 🏃 **Ejecutar el Proyecto**
Para correr el backend, usa el siguiente comando:

```bash
dotnet run
```

Esto iniciará el servidor en **`http://localhost:5000`** o **`https://localhost:5001`**.

---

## 🛠 **Pruebas con Postman**
Puedes importar el siguiente archivo JSON en Postman para probar las APIs más fácilmente:
- [Postman Collection](https://www.postman.com/)

### 🔹 **Endpoints Disponibles**
#### 📌 Préstamos (`/api/Prestamo`)
| Método | Endpoint                | Descripción                     |
|--------|-------------------------|---------------------------------|
| GET    | `/api/Prestamo`         | Obtener todos los préstamos    |
| GET    | `/api/Prestamo/{id}`    | Obtener un préstamo por ID     |
| POST   | `/api/Prestamo`         | Crear un nuevo préstamo        |
| PUT    | `/api/Prestamo/{id}`    | Actualizar un préstamo         |
| DELETE | `/api/Prestamo/{id}`    | Eliminar un préstamo           |

#### 📌 Plan de Pagos (`/api/PlanPagos`)
| Método | Endpoint                             | Descripción                           |
|--------|--------------------------------------|---------------------------------------|
| GET    | `/api/PlanPagos/{prestamoId}`       | Obtener plan de pagos de un préstamo |
| POST   | `/api/PlanPagos/{prestamoId}/generar` | Generar plan de pagos               |
| DELETE | `/api/PlanPagos/{prestamoId}/eliminar` | Eliminar plan de pagos               |

---

## 🏗 **Estructura del Proyecto**
```
📂 PrestamosService
 ┣ 📂 Controllers
 ┃ ┣ 📄 PrestamoController.cs
 ┃ ┣ 📄 PlanPagosController.cs
 ┣ 📂 Models
 ┃ ┣ 📄 Prestamo.cs
 ┃ ┣ 📄 PlanPago.cs
 ┣ 📂 Repositories
 ┃ ┣ 📄 IPrestamoRepository.cs
 ┃ ┣ 📄 PrestamoRepository.cs
 ┃ ┣ 📄 IPlanPagosRepository.cs
 ┃ ┣ 📄 PlanPagosRepository.cs
 ┣ 📂 Data
 ┃ ┣ 📄 ApplicationDbContext.cs
 ┣ 📄 Program.cs
 ┣ 📄 appsettings.json
 ┣ 📄 README.md
```

---

## 📢 **Notas**
- **⚠️ Importante**: Si tienes problemas con la cadena de conexión, verifica que tu servidor SQL esté corriendo y que el usuario tenga los permisos correctos.
- Se recomienda probar los endpoints con **Postman** o **Swagger** (`/swagger` en el navegador).

---

## 📌 **Contacto**
Si tienes dudas, puedes escribirme a [dervinardanihernandezluna2001@gmail.com] o en [GitHub](https://github.com/dervinluna). 🚀

---

---

✅ **Este README incluye:**
1. **Pasos detallados** para instalar y correr el backend.
2. **Configuración de la base de datos** en `appsettings.json`.
3. **Cómo ejecutar las migraciones** con `dotnet ef`.
4. **Lista de endpoints** con descripciones.
5. **Estructura del proyecto**.
6. **Notas y consejos** para solucionar problemas comunes.
