param location string = resourceGroup().location

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
