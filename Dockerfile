FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore
    
COPY . ./

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet build -c Release

RUN dotnet publish -c Release -o out

COPY entrypoint.sh .
RUN chmod +x ./entrypoint.sh

EXPOSE 5030

ENTRYPOINT ["./entrypoint.sh"]