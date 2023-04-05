param location string = resourceGroup().location

resource suiteUI 'Microsoft.Web/staticSites@2022-03-01'= {
  name: 'SuiteUI'
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

resource suiteUICustomerDomain 'Microsoft.Web/staticSites/customDomains@2022-03-01' = {
  name: 'www.mad-world.nl'
  parent: suiteUI
  properties: {}
}

resource adminUI 'Microsoft.Web/staticSites@2022-03-01'= {
  name: 'AdminUI'
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

resource adminUICustomerDomain 'Microsoft.Web/staticSites/customDomains@2022-03-01' = {
  name: 'admin.mad-world.nl'
  parent: adminUI
  properties: {}
}
