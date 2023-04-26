param apiManagementName string = 'madworld-api-management'

resource service 'Microsoft.ApiManagement/service@2022-08-01' existing = {
  name: apiManagementName
}

resource authorizedApi 'Microsoft.ApiManagement/service/apis@2022-08-01' existing = {
  name: 'madworld-api-authorized'
  parent: service
}

resource authorziedPolicy 'Microsoft.ApiManagement/service/apis/policies@2022-08-01' = {
  name: 'policy'
  parent: authorizedApi
  properties: {
    value: loadTextContent('./Policy/Authorized/AuthorizedApi.xml')
    format: 'rawxml'
  }
}

resource getPingOperation 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'get-ping'
  parent: authorizedApi
  properties: {
    displayName: 'Ping'
    method: 'GET'
    urlTemplate: '/Ping'
    templateParameters: []
    responses: []
  }
}

resource getPingPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2022-08-01' = {
  name: 'policy'
  parent: getPingOperation
  properties: {
    value: loadTextContent('./Policy/Authorized/GetPing.xml')
    format: 'rawxml'
  }
}

resource postPingOperation 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'post-ping'
  parent: authorizedApi
  properties: {
    displayName: 'Ping'
    method: 'POST'
    urlTemplate: '/Ping'
    templateParameters: []
    responses: []
  }
}

resource postPingPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2022-08-01' = {
  name: 'policy'
  parent: postPingOperation
  properties: {
    value: loadTextContent('./Policy/Authorized/PostPing.xml')
    format: 'rawxml'
  }
}
