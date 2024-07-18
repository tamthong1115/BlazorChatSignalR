FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BlazorChatSignalR/BlazorChatSignalR/BlazorChatSignalR.csproj", "BlazorChatSignalR/BlazorChatSignalR/"]
COPY ["BlazorChatSignalR/BlazorChatSignalR.Client/BlazorChatSignalR.Client.csproj", "BlazorChatSignalR/BlazorChatSignalR.Client/"]
RUN dotnet restore "BlazorChatSignalR/BlazorChatSignalR/BlazorChatSignalR.csproj"
COPY . .
WORKDIR "/src/BlazorChatSignalR/BlazorChatSignalR"
RUN dotnet build "BlazorChatSignalR.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BlazorChatSignalR.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorChatSignalR.dll"]
