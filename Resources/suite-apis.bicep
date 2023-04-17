param location string = resourceGroup().location
@secure()
param anonymousApiKey string
@secure()
param authorizedApiKey string

module apiAnonymous './azure-function.bicep' = {
  name: 'apiAnonymous'
  params: {
    location: location
    azureFunctionName: 'madworld-api-anonymous'
    serverFarmName: 'ASP-MadWorldSuite-8a87'
  }
}

module apiAuthorized './azure-function.bicep' = {
  name: 'apiAuthorized'
  params: {
    location: location
    azureFunctionName: 'madworld-api-authorized'
    serverFarmName: 'ASP-MadWorldSuite-a137'
  }
}

module apiManagement './ApiManagement/azure-api-management.bicep' =  {
  name: 'apiManagement'
  params: {
    location: location
    apiManagementName: 'madworld-api-management'
    anonymousApiKey: anonymousApiKey
    authorizedApiKey: authorizedApiKey
  }
}
