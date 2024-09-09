# Hotel Clone API

Este proyecto es una API desarrollada con ASP.NET Core para el proyecto Hotel Clone. Su propósito es proporcionar las funcionalidades necesarias para realizar operaciones CRUD desde el backend de la aplicación web.

## Requisitos

- [.NET Core SDK](https://dotnet.microsoft.com/download) (versión 5.0 o superior)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (o cualquier otra base de datos que utilices)

## Configuración

1. **Clona el repositorio:**

    ```bash
    git clone https://github.com/tu-usuario/hotel-clone-api.git
    ```

2. **Navega al directorio del proyecto:**

    ```bash
    cd hotel-clone-api
    ```

3. **Restaurar paquetes NuGet:**

    ```bash
    dotnet restore
    ```

4. **Configurar la cadena de conexión:**

    - Abre `appsettings.json` y configura la cadena de conexión a tu base de datos SQL Server.

5. **Aplicar migraciones (si usas Entity Framework Core):**

    ```bash
    dotnet ef database update
    ```

## Ejecución del Servidor

Ejecuta el siguiente comando para iniciar la API:

```bash
dotnet run
