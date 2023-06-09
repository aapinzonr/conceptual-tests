Pasos Creacion Base de Datos con Code First

1. Crear modelo de clases
2. Crear clase DataContext con colecciones que representan tablas
3. Configurar cadena de conexion a base de datos (sobrescribir metodo OnConfiguring)
4. Crear script de migracion (PM Console: add-migration CreateDb; dotnet Console: dotnet ef migrations add CreateDb)
	cd <contextProjectFolder>
	dotnet ef migrations add <migrationScriptName> -s <startupProjectFullFileName>
	Ejemplo: dotnet ef migrations add CreateDb -s D:\s\Sofka\TZ\talentzonelabs\Sofka.TalentZone.InventoryLab\Sofka.TalentZone.Inventory.API\Sofka.TalentZone.Inventory.API.csproj
	
5. Ejecutar script de migracion (PM Console: update-database –verbose; dotnet Console: dotnet ef database update)
	cd <contextProjectFolder>
	dotnet ef database update -s <startupProjectFullFileName>
	Ejemplo: dotnet ef database update -s D:\s\Sofka\TZ\talentzonelabs\Sofka.TalentZone.InventoryLab\Sofka.TalentZone.Inventory.API\Sofka.TalentZone.Inventory.API.csproj