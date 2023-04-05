param location string = resourceGroup().location

module suiteUI './static-site.bicep' = {
  name: 'suiteUI'
  params: {
    location: location
    name: 'SuiteUI'
    domainName: 'www.mad-world.nl'
  }
}

module adminUI './static-site.bicep' = {
  name: 'adminUI'
  params: {
    location: location
    name: 'AdminUI'
    domainName: 'admin.mad-world.nl'
  }
}
