# SQL Server 2022 with automatic database initialization
FROM mcr.microsoft.com/mssql/server:2022-latest

# Accept build arguments for secure credential handling
ARG SA_PASSWORD=yourStrongPassword#

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=${SA_PASSWORD}
ENV MSSQL_PID=Developer
ENV MSSQL_TCP_PORT=1433

WORKDIR /src
EXPOSE 1433

# Copy the database creation script
COPY createdb.sql ./filldata.sql

# Initialize the database during container build using environment variable
RUN (/opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "Service Broker manager has started" && \
    /opt/mssql-tools/bin/sqlcmd -S127.0.0.1 -Usa -P"${SA_PASSWORD}" -i filldata.sql
