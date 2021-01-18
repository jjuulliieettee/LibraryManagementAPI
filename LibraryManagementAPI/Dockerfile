#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["./LibraryManagementAPI.WebApi.csproj", "LibraryManagementAPI/"]
COPY ["LibraryManagementAPI.Notifications/LibraryManagementAPI.Notifications.csproj", "LibraryManagementAPI.Notifications/"]
COPY ["LibraryManagementAPI.Core/LibraryManagementAPI.Core.csproj", "LibraryManagementAPI.Core/"]
COPY ["LibraryManagementAPI.Data/LibraryManagementAPI.Data.csproj", "LibraryManagementAPI.Data/"]
RUN dotnet restore "LibraryManagementAPI/LibraryManagementAPI.WebApi.csproj"
COPY . .
WORKDIR "/src/LibraryManagementAPI"
RUN dotnet build "LibraryManagementAPI.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LibraryManagementAPI.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibraryManagementAPI.WebApi.dll"]