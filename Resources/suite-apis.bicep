param location string = resourceGroup().location
param identityName string
param workspaceName string
param smartDectectionName string
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
    workspaceName: workspaceName
    healthCheckName: 'health check-madworld-api-anonymous'
    healthCheckEndpoint: 'https://api.mad-world.nl/anonymous/healthcheck'
    smartDectectionName: smartDectectionName
    identityName: identityName
  }
}

module apiAuthorized './azure-function.bicep' = {
  name: 'apiAuthorized'
  params: {
    location: location
    azureFunctionName: authorizedApiName
    serverFarmName: 'ASP-MadWorldSuite-a137'
    workspaceName: workspaceName
    healthCheckName: 'health check-madworld-api-authorized'
    healthCheckEndpoint: 'https://api.mad-world.nl/authorized/healthcheck'
    smartDectectionName: smartDectectionName
    identityName: identityName
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
    resourceManager: environment().resourceManager
  }
}
