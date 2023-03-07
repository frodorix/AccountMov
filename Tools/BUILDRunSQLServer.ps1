#$source = "src/db"
#$destino = "Tools/ServerMSSQL"

#Copy-Item -Path $source -Filter "*.sql" -Recurse -Destination $destino -Container -force

#######elimiar imagen anterior
docker rmi --force $(docker images 'server-mssql' -q)

docker build -t server-mssql MSSql\.

#iniciar container
docker run -d -p 8433:1433 server-mssql
