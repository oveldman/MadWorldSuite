param location string = resourceGroup().location
param smartDetectionName string = 'Application Insights Smart Detection'
@secure()
param anonymousApiKey string 
@secure()
param authorizedApiKey string

module identity './managed-identity.bicep' = {
  name: 'identity'
  params: {
    name: 'MadWorldIdentity'
    location: location
  }
}

module blazorUIs './suite-uis.bicep' = {
  name: 'blazorUIs'
  params: {
    location: location
  }
}

module functionsAPIs './suite-apis.bicep' = {
  name: 'functionsAPIs'
  params: {
    location: location
    anonymousApiName: 'madworld-api-anonymous'
    anonymousApiKey: anonymousApiKey
    authorizedApiName: 'madworld-api-authorized'
    authorizedApiKey: authorizedApiKey
    smartDectectionName: smartDetectionName
  }
}

module storage './storage.bicep' = {
  name: 'storage'
  params: {
    location: location
  }
}

module applicationInsightSmartDetection './application-insights-smart-detection.bicep' = {
  name: 'applicationInsightSmartDetection'
  params: {
    name: smartDetectionName
  }
}
