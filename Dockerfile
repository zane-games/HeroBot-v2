FROM mcr.microsoft.com/dotnet/core/sdk as build
WORKDIR /build
COPY . .
RUN bash ./ci/build-all.sh


FROM mcr.microsoft.com/dotnet/core/runtime as runtime
WORKDIR /herobot
COPY --from=build /build/build .
ENTRYPOINT ["dotnet","exec","HeroBot.Core.dll"]
