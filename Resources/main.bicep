param location string = resourceGroup().location
param identityName string = 'MadWorldIdentity'
param workspaceName string = 'DefaultWorkspaceMadWorld'
param smartDetectionName string = 'Application Insights Smart Detection'
@secure()
param anonymousApiKey string
@secure()
param authorizedApiKey string

module workspace './workspace.bicep' = {
  name: 'workspace'
  params: {
    workspaces_DefaultWorkspaceMadWorld_name: workspaceName
    location: location
  }
}

module identity './managed-identity.bicep' = {
  name: 'identity'
  params: {
    name: identityName
    location: location
  }
}

module blazorUIs './suite-uis.bicep' = {
  name: 'blazorUIs'
  params: {
    location: location
  }
}

module keyVault './key-vault.bicep' = {
  name: 'keyVault'
  params: {
    location: location
    name: 'MadWorld-KeyVault'
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
    workspaceName: workspaceName
    smartDectectionName: smartDetectionName
    identityName: identityName
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
