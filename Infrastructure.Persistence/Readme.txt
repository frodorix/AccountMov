Dentro de la carpeta tools se encuentra un script para crear el container de la Base de datos



dotnet ef dbcontext scaffold "Data Source=localhost,8443;Initial Catalog=AccountMovs;Persist Security Info=True;User ID=sa;Password=yourStrongPassword#" Microsoft.EntityFrameworkCore.SqlServer --namespace com.frodorix.bank --context-dir Contexts --output-dir Entity/Accounts --data-annotations  --table Cliente --table Cuenta --table Movimientos
