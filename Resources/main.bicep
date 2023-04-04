param blazorUIName string = 'UI'
param location string = resourceGroup().location

resource blazorUI 'Microsoft.Web/staticSites@2022-03-01'= {
  name: blazorUIName
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

resource blazorUICustomerDomain 'Microsoft.Web/staticSites/customDomains@2022-03-01' = {
  name: '${blazorUIName}-www.mad-world.nl'
  parent: blazorUI
  properties: { }
}
