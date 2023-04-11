param location string = resourceGroup().location

module apiAnonymous './AzureFunctions/azure-function.bicep' = {
  name: 'apiAnonymous'
  params: {
    location: location
    azureFunctionName: 'madworld-api-anonymous'
    projectNamespace: 'MadWorld.Backend.API.Anonymous'
    serverFarmName: 'ASP-MadWorldSuite-8a87'
  }
}

module apiAuthorized './AzureFunctions/azure-function.bicep' = {
  name: 'apiAuthorized'
  params: {
    location: location
    azureFunctionName: 'madworld-api-authorized'
    projectNamespace: 'MadWorld.Backend.API.Authorized'
    serverFarmName: 'ASP-MadWorldSuite-a137'
  }
}
