# Instrucciones para Compilar y Ejecutar el Proyecto API

Este repositorio contiene un proyecto en .Net 6 en C# que puedes utilizar como base para tus propias aplicaciones web. Aquí están los pasos básicos para compilar y ejecutar el proyecto:

Requisitos Previos
Antes de comenzar, asegúrate de tener instalado lo siguiente:
 - Visual Studio 2022
 - .NET 6.0 SDK
 - Cambiar la conexión de base de datos con una valida en su entorno local en el archivo appsettings.json
```bash
"ConnectionStrings": {
    "DefaultConnection": "Server=DESKTOP-G7VVON7;Initial Catalog=DB_REDBROW;Persist Security Info=False;User ID=sa;Password=xxxxxxxxxxx;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;"
  }
```
 - Instalar la base de datos ejecutando lo siguiente en consola el proyecto :
```bash
Update-Database
```

Pasos para Compilar y Ejecutar
1. Clona el Repositorio:
```bash
git clone https://github.com/pcornelio/frontend.git
cd frontend
```
2. Compila y ejecuta el proyecto: 
```bash
dotnet build
dotnet run
```
