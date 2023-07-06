param location string = resourceGroup().location
param azureFunctionName string
param serverFarmName string
param healthCheckName string
param healthCheckEndpoint string
param smartDectectionName string
param identityName string
param workspaceName string

resource identity 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-01-31' existing = {
  name: identityName
}

resource serverFarm 'Microsoft.Web/serverfarms@2022-09-01' = {
  name: serverFarmName
  location: location
  sku: {
    name: 'Y1'
    tier: 'Dynamic'
    size: 'Y1'
    family: 'Y'
    capacity: 0
  }
  kind: 'functionapp'
  properties: {
    perSiteScaling: false
    elasticScaleEnabled: false
    maximumElasticWorkerCount: 1
    isSpot: false
    reserved: true
    isXenon: false
    hyperV: false
    targetWorkerCount: 0
    targetWorkerSizeId: 0
    zoneRedundant: false
  }
}

module applicationInsight './application-insight.bicep' = {
  name: 'applicationInsight-${azureFunctionName}'
  params: {
    insightName: azureFunctionName
    location: location
    workspaceName: workspaceName
    healthCheckName: healthCheckName
    healthCheckEndpoint: healthCheckEndpoint
    smartDectectionName: smartDectectionName
  }
}

resource applicationInsightResource 'Microsoft.Insights/components@2020-02-02' existing = {
  name: azureFunctionName
}

resource api 'Microsoft.Web/sites@2022-09-01' = {
  name: azureFunctionName
  location: location
  tags: {
    'hidden-link:${applicationInsightResource.id}': 'Resource'
  }
  kind: 'functionapp,linux'
  properties: {
    enabled: true
    hostNameSslStates: [
      {
        name: '${azureFunctionName}.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Standard'
      }
      {
        name: '${azureFunctionName}.scm.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Repository'
      }
    ]
    serverFarmId: serverFarm.id
    reserved: true
    isXenon: false
    hyperV: false
    vnetRouteAllEnabled: false
    vnetImagePullEnabled: false
    vnetContentShareEnabled: false
    siteConfig: {
      numberOfWorkers: 1
      linuxFxVersion: 'DOTNET-ISOLATED|7.0'
      acrUseManagedIdentityCreds: false
      alwaysOn: false
      http20Enabled: false
      functionAppScaleLimit: 200
      minimumElasticInstanceCount: 0
    }
    scmSiteAlsoStopped: false
    clientAffinityEnabled: false
    clientCertEnabled: false
    clientCertMode: 'Required'
    hostNamesDisabled: false
    customDomainVerificationId: 'B7C4F943190F39D90EFD0885099BFD1708B6C22A30703F309CE17F6158349C15'
    containerSize: 1536
    dailyMemoryTimeQuota: 0
    httpsOnly: true
    redundancyMode: 'None'
    publicNetworkAccess: 'Enabled'
    storageAccountRequired: false
    keyVaultReferenceIdentity: identity.id
  }
}

resource credentialsPoliciesFtp 'Microsoft.Web/sites/basicPublishingCredentialsPolicies@2022-09-01' = {
  name: 'ftp'
  parent: api
  location: location
  tags: {
    'hidden-link:${applicationInsightResource.id}': 'Resource'
  }
  properties: {
    allow: true
  }
}

resource credentialsPoliciesScm 'Microsoft.Web/sites/basicPublishingCredentialsPolicies@2022-09-01' = {
  name: 'scm'
  parent: api
  location: location
  tags: {
    'hidden-link:${applicationInsightResource.id}': 'Resource'
  }
  properties: {
    allow: true
  }
}

resource apiManagement 'Microsoft.ApiManagement/service@2022-08-01' existing = {
  name: 'madworld-api-management'
}

resource webConfig 'Microsoft.Web/sites/config@2022-09-01' = {
  name: 'web'
  parent: api
  location: location
  tags: {
    'hidden-link:${applicationInsightResource.id}': 'Resource'
  }
  properties: {
    numberOfWorkers: 1
    defaultDocuments: [
      'Default.htm'
      'Default.html'
      'Default.asp'
      'index.htm'
      'index.html'
      'iisstart.htm'
      'default.aspx'
      'index.php'
    ]
    netFrameworkVersion: 'v4.0'
    linuxFxVersion: 'DOTNET-ISOLATED|7.0'
    requestTracingEnabled: false
    remoteDebuggingEnabled: false
    httpLoggingEnabled: false
    acrUseManagedIdentityCreds: false
    logsDirectorySizeLimit: 35
    detailedErrorLoggingEnabled: false
    publishingUsername: '$${azureFunctionName}'
    scmType: 'None'
    use32BitWorkerProcess: true
    webSocketsEnabled: false
    alwaysOn: false
    managedPipelineMode: 'Integrated'
    virtualApplications: [
      {
        virtualPath: '/'
        physicalPath: 'site\\wwwroot'
        preloadEnabled: false
      }
    ]
    loadBalancing: 'LeastRequests'
    experiments: {
      rampUpRules: []
    }
    autoHealEnabled: false
    vnetRouteAllEnabled: false
    vnetPrivatePortsCount: 0
    publicNetworkAccess: 'Enabled'
    cors: {
      allowedOrigins: [
        'https://portal.azure.com'
        'https://functions.azure.com'
      ]
      supportCredentials: false
    }
    apiManagementConfig: {
      id: '${apiManagement.id}/apis/${azureFunctionName}'
    }
    localMySqlEnabled: false
    ipSecurityRestrictions: [
      {
        ipAddress: 'Any'
        action: 'Allow'
        priority: 2147483647
        name: 'Allow all'
        description: 'Allow all access'
      }
    ]
    scmIpSecurityRestrictions: [
      {
        ipAddress: 'Any'
        action: 'Allow'
        priority: 2147483647
        name: 'Allow all'
        description: 'Allow all access'
      }
    ]
    scmIpSecurityRestrictionsUseMain: false
    http20Enabled: false
    minTlsVersion: '1.2'
    scmMinTlsVersion: '1.2'
    ftpsState: 'FtpsOnly'
    preWarmedInstanceCount: 0
    functionAppScaleLimit: 200
    functionsRuntimeScaleMonitoringEnabled: false
    minimumElasticInstanceCount: 0
    azureStorageAccounts: {}
  }
}

resource hostBindings 'Microsoft.Web/sites/hostNameBindings@2022-09-01' = {
  name: '${azureFunctionName}.azurewebsites.net'
  parent: api
  properties: {
    siteName: azureFunctionName
    hostNameType: 'Verified'
  }
}
