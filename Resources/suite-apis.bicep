param location string = resourceGroup().location
param smartDectectionName string = 'Application Insights Smart Detection'
param anonymousApiName string
@secure()
param anonymousApiKey string
param authorizedApiName string
@secure()
param authorizedApiKey string

module apiAnonymous './azure-function.bicep' = {
  name: 'apiAnonymous'
  params: {
    location: location
    azureFunctionName: anonymousApiName
    serverFarmName: 'ASP-MadWorldSuite-8a87'
    healthCheckName: 'health check-madworld-api-anonymous'
    healthCheckEndpoint: 'https://api.mad-world.nl/anonymous/healthcheck'
    smartDectectionName: smartDectectionName
  }
}

module apiAuthorized './azure-function.bicep' = {
  name: 'apiAuthorized'
  params: {
    location: location
    azureFunctionName: authorizedApiName
    serverFarmName: 'ASP-MadWorldSuite-a137'
    healthCheckName: 'health check-madworld-api-authorized'
    healthCheckEndpoint: 'https://api.mad-world.nl/authorized/healthcheck'
    smartDectectionName: smartDectectionName
  }
}

module apiManagement './ApiManagement/azure-api-management.bicep' =  {
  name: 'apiManagement'
  params: {
    location: location
    apiManagementName: 'madworld-api-management'
    anonymousApiName: anonymousApiName
    anonymousApiKey: anonymousApiKey
    authorizedApiName: authorizedApiName
    authorizedApiKey: authorizedApiKey
  }
}
