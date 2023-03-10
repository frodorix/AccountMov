#$source = "src/db"
#$destino = "Tools/ServerMSSQL"
$network="frodorix-account-network" 
$sqlservercontainer="frodorix-sqlserver"
$apicontainer="frodorix-account-api"
$apiimage="frodo-account-api:latest"
$sqlimage="frodo-mssql-image:latest"

docker network create -d bridge $network

#Copy-Item -Path $source -Filter "*.sql" -Recurse -Destination $destino -Container -force
echo "##############################################"
echo "######  CREAMOS SQL SERVER CONTAINER #########"
echo "##############################################"
#######elimiar imagen anterior
$haySQL=$(docker images $sqlimage -q)
if($haySQL){
	docker rmi --force $haySQL
}

docker build -t $sqlimage MSSql\.

#iniciar container
docker run -d --name $sqlservercontainer  -p 8433:1433 --network $network $sqlimage

echo "##############################################"
echo "######     AHORA VAMOS POR LA API    #########"
echo "##############################################"

#PREPARA API CONTAINER
$hayAPI=$(docker images '$apiimage' -q)

if($hayAPI){
	docker rmi --force $hayAPI
}
cd ..
docker build -t $apiimage -f .\AccountMovAPI\Dockerfile .
##iniciar API container
docker run -d --name $apicontainer -p 8432:	 --network $network $apiimage		
cd Tools

echo "##############################################"
echo "######           TERMIAMOS           #########"
echo "##############################################"
