FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

COPY . /app
RUN dotnet publish -c Release -p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:5.0
LABEL maintainer="trickybestia@gmail.com"
LABEL org.label-schema.schema-version="1.0"
LABEL org.label-schema.url="https://github.com/TrickyBestia/TrickyBot"
LABEL org.label-schema.vcs-url="https://github.com/TrickyBestia/TrickyBot"
LABEL org.label-schema.version="3.0.0"
LABEL org.label-schema.docker.cmd="docker run -d --name trickybot --mount type=bind,source=/var/TrickyBotData,target=/appdata trickybestia/trickybot:3.0.0"
WORKDIR /app

COPY --from=build /app/TrickyBot.Core/bin/Release/publish .
RUN mkdir /appdata && useradd trickybot -M && chown -R trickybot . /appdata
ENTRYPOINT ["dotnet", "TrickyBot.Core.dll", "--data", "/appdata"]
USER trickybot