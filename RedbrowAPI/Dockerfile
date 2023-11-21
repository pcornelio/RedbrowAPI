#Start with the base .NET SDK Image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

#Set the working directory inside the container
WORKDIR webapi
EXPOSE 7288
EXPOSE 5289

#Copy the project files and restore the depencies
COPY ./*.csproj ./
RUN dotnet restore

#Copy the remainign files and build the application
COPY . .
RUN dotnet publish -c Release -o out

#Build the runtime image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS runtime-env
WORKDIR /webapi
COPY --from=build-env /webapi/out .

#set entry point 
ENTRYPOINT ["dotnet", "RedbrowAPI.dll"]