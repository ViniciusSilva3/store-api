# build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /source
COPY . ./
RUN dotnet restore "./Catalog/Catalog.csproj" --disable-parallel

#RUN dotnet build "./Catalog/Catalog.csproj" -c Release -o /app/build
# all the artifacts from publish will end up in /app directory
RUN dotnet build "./Catalog/Catalog.csproj" -c Release -o /app/build

FROM build-env AS publish
RUN dotnet publish "./Catalog/Catalog.csproj" -c Release -o /app/publish
# serve stage

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=publish /app/publish .

# # EXPOSE 5000

ENTRYPOINT ["dotnet", "Catalog.dll"]
# ENTRYPOINT ["tail", "-f", "/dev/null"]
#ENTRYPOINT /bin/bash