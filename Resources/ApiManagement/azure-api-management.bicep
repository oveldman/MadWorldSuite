param location string = resourceGroup().location
param resourceManager string = environment().resourceManager
param apiManagementName string = 'madworld-api-management'
param anonymousApiName string
@secure()
param anonymousApiKey string
param authorizedApiName string
@secure()
param authorizedApiKey string

resource apiManagement 'Microsoft.ApiManagement/service@2019-01-01' = {
  name: apiManagementName
  location: location
  sku: {
    name: 'Consumption'
    capacity: 0
  }
  properties: {
    publisherEmail: 'oveldman@gmail.com'
    publisherName: 'Mad-World'
    notificationSenderEmail: 'apimgmt-noreply@mail.windowsazure.com'
    hostnameConfigurations: [
      {
        type: 'Proxy'
        hostName: '${apiManagementName}.azure-api.net'
        negotiateClientCertificate: false
        defaultSslBinding: true
      }
    ]
    customProperties: {
        'Microsoft.WindowsAzure.ApiManagement.Gateway.Security.Protocols.Tls11': 'false'
        'Microsoft.WindowsAzure.ApiManagement.Gateway.Security.Protocols.Tls10': 'false'
        'Microsoft.WindowsAzure.ApiManagement.Gateway.Security.Backend.Protocols.Tls11': 'false'
        'Microsoft.WindowsAzure.ApiManagement.Gateway.Security.Backend.Protocols.Tls10': 'false'
        'Microsoft.WindowsAzure.ApiManagement.Gateway.Security.Backend.Protocols.Ssl30': 'false'
        'Microsoft.WindowsAzure.ApiManagement.Gateway.Protocols.Server.Http2': 'false'
    }
    virtualNetworkType: 'None'
  }
}

resource anonymousApi 'Microsoft.ApiManagement/service/apis@2022-08-01' = {
  name: anonymousApiName
  parent: apiManagement
  properties: {
    displayName: anonymousApiName
    apiRevision: '1'
    description: 'Import from "${anonymousApiName}" Function App'
    subscriptionRequired: false
    path: 'anonymous'
    protocols: [
      'https'
    ]
    authenticationSettings: {
      oAuth2AuthenticationSettings: []
      openidAuthenticationSettings: []
    }
    subscriptionKeyParameterNames: {
      header: 'Ocp-Apim-Subscription-Key'
      query: 'subscription-key'
    }
    isCurrent: true
  }
}

resource authorizedApi 'Microsoft.ApiManagement/service/apis@2022-08-01' = {
  name: authorizedApiName
  parent: apiManagement
  properties: {
    displayName: authorizedApiName
    apiRevision: '1'
    description: 'Import from "${authorizedApiName}" Function App'
    subscriptionRequired: false
    path: 'authorized'
    protocols: [
      'https'
    ]
    authenticationSettings: {
      oAuth2AuthenticationSettings: []
      openidAuthenticationSettings: []
    }
    subscriptionKeyParameterNames: {
      header: 'Ocp-Apim-Subscription-Key'
      query: 'subscription-key'
    }
    isCurrent: true
  }
}

resource anonymousBackend 'Microsoft.ApiManagement/service/backends@2022-08-01' = {
  name: anonymousApiName
  parent: apiManagement
  properties: {
    description: anonymousApiName
    url: 'https://${anonymousApiName}.azurewebsites.net/api'
    protocol: 'http'
    resourceId: '${resourceManager}subscriptions/${az.subscription().id}/resourceGroups/${resourceGroup().name}/providers/Microsoft.Web/sites/${anonymousApiName}'
    credentials: {
      header: {
        'x-functions-key': [ '{{${anonymousApiName}-key}}' ]
      }
    }
  }
}

resource authorizedBackend 'Microsoft.ApiManagement/service/backends@2022-08-01' = {
  name: authorizedApiName
  parent: apiManagement
  properties: {
    description: authorizedApiName
    url: 'https://${authorizedApiName}.azurewebsites.net/api'
    protocol: 'http'
    resourceId: '${resourceManager}subscriptions/${az.subscription().id}/resourceGroups/${resourceGroup().name}/providers/Microsoft.Web/sites/${authorizedApiName}'
    credentials: {
      header: {
        'x-functions-key': [ '{{${authorizedApiName}-key}}' ]
      }
    }
  }
}

resource anonymousNameValues 'Microsoft.ApiManagement/service/namedValues@2022-08-01' = {
  name: 'madworld-api-anonymous-key'
  parent: apiManagement
  properties: {
    displayName: 'madworld-api-anonymous-key'
    value: anonymousApiKey
    tags: [
      'key', 'function', 'auto'
    ]
  }
}

resource authorizedNameValues 'Microsoft.ApiManagement/service/namedValues@2022-08-01' = {
  name: 'madworld-api-authorized-key'
  parent: apiManagement
  properties: {
    displayName: 'madworld-api-authorized-key'
    value: anonymousApiKey
    tags: [
      'key', 'function', 'auto'
    ]
  }
}

resource apiManagementPolicy 'Microsoft.ApiManagement/service/policies@2022-08-01' = {
  name: 'policy'
  parent: apiManagement
  properties: {
    value: loadTextContent('./Policy/GeneralApi.xml')
    format: 'rawxml'
  }
}

resource anonumousProperty 'Microsoft.ApiManagement/service/properties@2019-01-01' = {
  name: 'madworld-api-anonymous-key'
  parent: apiManagement
  properties: {
    displayName: 'madworld-api-anonymous-key'
    value: anonymousApiKey
    tags: [
      'key', 'function', 'auto' 
    ]
    secret: true
  }
}

resource authorizedProperty 'Microsoft.ApiManagement/service/properties@2019-01-01' = {
  name: 'madworld-api-authorized-key'
  parent: apiManagement
  properties: {
    displayName: 'madworld-api-authorized-key'
    value: authorizedApiKey
    tags: [
      'key', 'function', 'auto' 
    ]
    secret: true
  }
}

resource subscription 'Microsoft.ApiManagement/service/subscriptions@2022-08-01' = {
  name: 'master'
  parent: apiManagement
  properties: {
    scope: '/'
    displayName: 'Built-in all-access subscription'
    state: 'active'
    allowTracing: false
  }
}

module anonymousEndpoints 'api-endpoints-anonymous.bicep' = {
  name: 'anonymousEndpoints'
  params: {
    apiManagementName: apiManagementName
  }
}

module authorizedEndpoints 'api-endpoints-authorized.bicep' = {
  name: 'authorizedEndpoints'
  params: {
    apiManagementName: apiManagementName
  }
}
