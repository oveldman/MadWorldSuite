[![SonarCloud](https://github.com/oveldman/MadWorldSuite/actions/workflows/sonarqube.yml/badge.svg)](https://github.com/oveldman/MadWorldSuite/actions/workflows/sonarqube.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=oveldman_MadWorldSuite&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=oveldman_MadWorldSuite)
[![Deploy_Azure_ARM](https://github.com/oveldman/MadWorldSuite/actions/workflows/azure-resources.yml/badge.svg?branch=main)](https://github.com/oveldman/MadWorldSuite/actions/workflows/azure-resources.yml)
[![Azure Suite UI CI/CD](https://github.com/oveldman/MadWorldSuite/actions/workflows/azure-frontend-suite-ui.yml/badge.svg)](https://github.com/oveldman/MadWorldSuite/actions/workflows/azure-frontend-suite-ui.yml)
[![Azure Admin UI CI/CD](https://github.com/oveldman/MadWorldSuite/actions/workflows/azure-frontend-admin-ui.yml/badge.svg)](https://github.com/oveldman/MadWorldSuite/actions/workflows/azure-frontend-admin-ui.yml)
[![Azure API Anonymous CI/CD](https://github.com/oveldman/MadWorldSuite/actions/workflows/azure-api-anonymous.yml/badge.svg)](https://github.com/oveldman/MadWorldSuite/actions/workflows/azure-api-anonymous.yml)
[![Azure API Authorized CI/CD](https://github.com/oveldman/MadWorldSuite/actions/workflows/azure-api-authorized.yml/badge.svg)](https://github.com/oveldman/MadWorldSuite/actions/workflows/azure-api-authorized.yml)
# MadWorldSuite
MadWorldSuite is a captivating hobby project that serves as a powerful platform for me to delve into the realms of Azure,
Blazor, and Azure Functions. As a passionate learner, I have embarked on this exciting journey to explore and master these 
cutting-edge technologies while building a collection of diverse tools, a personalized CV, and a host of other thrilling 
features that will be unveiled soon.

## Pre-requisites
Make sure you have installed all of the following prerequisites on your development machine:
* Rider - [Download & Install Rider](https://www.jetbrains.com/rider/download/#section=windows) or another IDE of your choice.
* Git - [Download & Install Git](https://git-scm.com/downloads). OSX and Linux machines typically have this already installed.
* Docker - [Download & Install Docker](https://www.docker.com/products/docker-desktop)
* Dotnet 7 - [Download & Install Dotnet 7](https://dotnet.microsoft.com/download/dotnet/7.0)
* Azurite - [Download & Install Azurite](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio)

## How to Install and Run the Project
First make sure that azurite is running:
``` shell
azurite -l /tmp/azurite # Use a temporary folder for storage
```

Set multiple startup projects:
* MadWorld.Backend.Api.Anonymous
* MadWorld.Backend.Api.Authorized
* MadWorld.Frontend.Admin.UI
* MadWorld.Frontend.Suite.UI

Set functions host arguments:
* MadWorld.Backend.Api.Anonymous
  * `host start --pause-on-error --cors * --port 7071`
* MadWorld.Backend.Api.Authorized
  * `host start --pause-on-error --cors * --port 7072`

Configure your AzureAd B2C your MadWorld.Frontend.UI.Suite configurations in `appsettings.Development.json`

Configure your AzureAd B2C your MadWorld.Frontend.UI.Admin configurations in `appsettings.Development.json`

Configure your AzureAd B2C in your MadWorld.Backend.API.Anonymous configurations by:
* Copy `local.settings.example.json` and name the file `local.settings.json`

Configure your AzureAd B2C in your MadWorld.Backend.API.Authorized configurations by:
* Copy `local.settings.example.json`, change the `<TEMP>` values and name the file `local.settings.json`

Run happily your project!

## How to Run the Tests
First make sure that Docker Desktop is running:
``` shell
# Windows
C:\Program Files\Docker\Docker\Docker Desktop.exe

# Linux
systemctl --user start docker-desktop

# MacOS
open -a Docker
```
Then run the following command to run all tests:
``` shell
cd MadWorld
dotnet test
```
When all unit and integration tests pass, you should observe the following output for each test project:
``` shell
Passed!  - Failed:     0, Passed:     2, Skipped:     0, Total:     2, 
Duration: 6 s - MadWorld.Backend.Api.Anonymous.IntegrationTests.dll (net7.0)
```

## Currently maintained by
* [Oscar Veldman](https://www.github.com/oveldman)
