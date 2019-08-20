FROM mcr.microsoft.com/dotnet/core/sdk:3.0 as build
WORKDIR /build
COPY . .
RUN bash ./ci/build-all.sh


FROM mcr.microsoft.com/dotnet/core/runtime:3.0 as runtime
WORKDIR /herobot
COPY --from=build /build/build .
ENTRYPOINT ["dotnet","exec","HeroBot.Core.dll"]
