FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["AmazingCo.Api/AmazingCo.Api.csproj", "AmazingCo.Api/"]
RUN dotnet restore "AmazingCo.Api/AmazingCo.Api.csproj"
COPY . .
WORKDIR "/src/AmazingCo.Api"
RUN dotnet build "AmazingCo.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AmazingCo.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AmazingCo.Api.dll"]