# Proyecto_BAD115_2024

* ******** PARA CONFIGURAR BASE DE DATOS EN EL PROYECTO *************
* ---------- Descargas necesarias para visualizar tablas en un SGBD (usaremos SSMS) -----------------

- SQL Server 2022 Express: https://go.microsoft.com/fwlink/p/?linkid=2216019&clcid=0x40A&culture=es-es&country=es
- SQL Server Management Studio (SSMS): https://aka.ms/ssmsfullsetup
- Visual Studio 2022: https://visualstudio.microsoft.com/es/thank-you-downloading-visual-studio/?sku=Community&channel=Release&version=VS2022&source=VSLandingPage&cid=2030&passive=false


#---------- Conf. SQL Server Management Studio -----------------
1. Crear conexion con Nuevo Servidor
2. Asignar configuraciones:

![image](https://github.com/Orellanna/Proyecto_BAD115_2024/assets/90300477/b8749cb6-30c7-4082-9e8a-a4e63f0bd3b1)


---------- Instalacion de Entity Framework en Visual Studio -----------------

1. Clic derecho en "Dependencias"
2. Clic en "Instalar Paquetes con Nugget"
3. Instalar: Microsoft.EntityFrameworkCore.SqlServer (6.0.1), Microsoft.Ent1tyFrameworkCore.Tools (6.0.1)  y Microsoft.AspNetCore.Identity.EntityFrameworkCore(6.0.1) ***Justo esas versiones

---------- Generacion de modelos -----------------

En consola de Nugget escribir lo siguiente:
Scaffold-DbContext "Server=bad-encuestas.database.windows.net; Database=sis-encuestas; User Id=lf18010; Password=Nueva2023!.;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
