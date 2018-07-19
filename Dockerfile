FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /src/QuestionsWereAsked.Server
EXPOSE 6969
ENTRYPOINT ["dotnet", "run"]
