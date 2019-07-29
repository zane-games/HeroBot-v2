FROM mcr.microsoft.com/dotnet/core/runtime:3.0
WORKDIR /app
ADD ./out/ /app
ENTRYPOINT ["dotnet","HeroBot.Core.dll"]
