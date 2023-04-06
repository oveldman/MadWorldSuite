param location string = resourceGroup().location

module blazorUIs './suite-uis.bicep' = {
  name: 'blazorUIs'
  params: {
    location: location
  }
}

module storage './storage.bicep' = {
  name: 'storage'
  params: {
    location: location
  }
}
