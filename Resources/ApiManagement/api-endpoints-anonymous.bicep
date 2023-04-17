param apiManagementName string = 'madworld-api-management'

resource service 'Microsoft.ApiManagement/service@2022-08-01' existing = {
  name: apiManagementName
}

resource anonymousApi 'Microsoft.ApiManagement/service/apis@2022-08-01' existing = {
  name: 'madworld-api-anonymous'
  parent: service
}

resource getPingOperation 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'get-ping'
  parent: anonymousApi
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
    value: loadTextContent('./Policy/Anonymous/GetPing.xml')
    format: 'rawxml'
  }
}

resource postPingOperation 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'post-ping'
  parent: anonymousApi
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
    value: loadTextContent('./Policy/Anonymous/PostPing.xml')
    format: 'rawxml'
  }
}
