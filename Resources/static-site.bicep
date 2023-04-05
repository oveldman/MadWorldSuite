param location string = resourceGroup().location
param name string = 'SuiteUI'
param domainName string = 'www.mad-world.nl'

resource staticWeb 'Microsoft.Web/staticSites@2022-03-01'= {
  name: name
  location: location
  sku: {
    name: 'Free'
    tier: 'Free'
  }
  properties: {
    repositoryUrl: 'https://github.com/oveldman/MadWorldSuite'
    branch: 'main'
    stagingEnvironmentPolicy: 'Enabled'
    allowConfigFileUpdates: true
    provider: 'Github'
    enterpriseGradeCdnStatus: 'Disabled'
  }
}

resource staticWebCustomerDomain 'Microsoft.Web/staticSites/customDomains@2022-03-01' = {
  name: domainName
  parent: staticWeb
  properties: {}
}
