docker pull mcr.microsoft.com/mssql/server:2022-latest
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=m4st3rk3yRedarbor" -p 1433:1433 --name SQLServer --hostname SQLServer -d mcr.microsoft.com/mssql/server:2022-latest
docker ps -a
docker start SQLServer