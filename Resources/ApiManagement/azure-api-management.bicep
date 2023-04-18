param location string = resourceGroup().location
param apiManagementName string = 'madworld-api-management'
@secure()
param anonymousApiKey string
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
  name: 'madworld-api-anonymous'
  parent: apiManagement
  properties: {
    displayName: 'madworld-api-anonymous'
    apiRevision: '1'
    description: 'Import from "madworld-api-anonymous" Function App'
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
  name: 'madworld-api-authorized'
  parent: apiManagement
  properties: {
    displayName: 'madworld-api-authorized'
    apiRevision: '1'
    description: 'Import from "madworld-api-authorized" Function App'
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

resource anonymousAzureFunctions 'Microsoft.Web/sites@2022-09-01' = {
  name: 'madworld-api-anonymous'
  location: location
}

resource anonymousBackend 'Microsoft.ApiManagement/service/backends@2022-08-01' = {
  name: 'madworld-api-anonymous'
  parent: apiManagement
  properties: {
    description: 'madworld-api-anonymous'
    url: 'https://madworld-api-anonymous.azurewebsites.net/api'
    protocol: 'http'
    resourceId: anonymousAzureFunctions.id
    credentials: {
      header: {
        'x-functions-key': [ '{{madworld-api-anonymous-key}}' ]
      }
    }
  }
}

resource authorizedAzureFunctions 'Microsoft.Web/sites@2022-09-01' = {
  name: 'madworld-api-authorized'
  location: location
}

resource authorizedBackend 'Microsoft.ApiManagement/service/backends@2022-08-01' = {
  name: 'madworld-api-authorized'
  parent: apiManagement
  properties: {
    description: 'madworld-api-authorized'
    url: 'https://madworld-api-authorized.azurewebsites.net/api'
    protocol: 'http'
    resourceId: authorizedAzureFunctions.id
    credentials: {
      header: {
        'x-functions-key': [ '{{madworld-api-authorized-key}}' ]
      }
    }
  }
}

/*

resource anonymousNameValues 'Microsoft.ApiManagement/service/namedValues@2022-08-01' = {
  name: 'madworld-api-anonymous-key'
  parent: apiManagement
  properties: {
    displayName: 'madworld-api-anonymous-key'
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
    tags: [
      'key', 'function', 'auto'
    ]
  }
}

*/

resource apiManagementPolicy 'Microsoft.ApiManagement/service/policies@2022-08-01' = {
  name: 'policy'
  parent: apiManagement
  properties: {
    value: loadTextContent('./Policy/GeneralApi.xml')
    format: 'rawxml'
  }
}

/*

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
    scope: '${apiManagement.id}${apiManagementName}/'
    displayName: 'Built-in all-access subscription'
    state: 'active'
    allowTracing: false
  }
}

*/

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
