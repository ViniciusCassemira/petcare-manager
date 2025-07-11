#!/bin/bash

export ASPNETCORE_ENVIRONMENT=${ASPNETCORE_ENVIRONMENT:-Production}
export ASPNETCORE_URLS=${ASPNETCORE_URLS:-http://+:5030}

echo "=== Iniciando aplicação ==="

echo "Executando migrations..."
cd /app
dotnet ef database update --configuration Release --no-build --verbose || exit 1
echo "Migrations executadas com sucesso!"

echo "Iniciando a aplicação..."
cd /app/out

echo "Executando: system-petshop.dll"
exec dotnet system-petshop.dll