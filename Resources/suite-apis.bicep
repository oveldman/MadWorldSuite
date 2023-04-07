param location string = resourceGroup().location

module apiAnonymous './azure-function.bicep' = {
  name: 'apiAnonymous'
  params: {
    location: location
    azureFunctionName: 'madworld-api-anonymous'
    serverFarmName: 'ASP-MadWorldSuite-a900'
  }
}

module apiAuthorized './azure-function.bicep' = {
  name: 'apiAuthorized'
  params: {
    location: location
    azureFunctionName: 'madworld-api-authorized'
    serverFarmName: 'ASP-MadWorldSuite-be2f'
  }
}
