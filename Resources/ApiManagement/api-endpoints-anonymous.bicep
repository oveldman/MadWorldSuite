param apiManagementName string

resource service 'Microsoft.ApiManagement/service@2022-08-01' existing = {
  name: apiManagementName
}

resource anonymousApi 'Microsoft.ApiManagement/service/apis@2022-08-01' existing = {
  name: 'madworld-api-anonymous'
  parent: service
}

resource getCurriculumVitae 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'get-curriculum-vitae'
  parent: anonymousApi
  properties: {
    displayName: 'GetCurriculumVitae'
    method: 'GET'
    urlTemplate: '/CurriculumVitae'
    templateParameters: []
    responses: []
  }
}

resource getCurriculumVitaePolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2022-08-01' = {
  name: 'policy'
  parent: getCurriculumVitae
  properties: {
    value: loadTextContent('./Policy/Anonymous/StandardEndpoint.xml')
    format: 'rawxml'
  }
}

resource getHealthCheckOperation 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'get-heatlh-check'
  parent: anonymousApi
  properties: {
    displayName: 'HealthCheck'
    method: 'GET'
    urlTemplate: '/HealthCheck'
    templateParameters: []
    responses: []
  }
}

resource getHealthCheckPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2022-08-01' = {
  name: 'policy'
  parent: getHealthCheckOperation
  properties: {
    value: loadTextContent('./Policy/Anonymous/StandardEndpoint.xml')
    format: 'rawxml'
  }
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

resource getStatusOperation 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'get-status'
  parent: anonymousApi
  properties: {
    displayName: 'GetStatus'
    method: 'GET'
    urlTemplate: '/GetStatus'
    templateParameters: []
    responses: []
  }
}

resource getStatusPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2022-08-01' = {
  name: 'policy'
  parent: getStatusOperation
  properties: {
    value: loadTextContent('./Policy/Anonymous/StandardEndpoint.xml')
    format: 'rawxml'
  }
}

resource getRenderOAuth2RedirectOperation 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'get-render-oAuth-2-Redirect'
  parent: anonymousApi
  properties: {
    displayName: 'RenderOAuth2Redirect'
    method: 'GET'
    urlTemplate: '/oauth2-redirect.html'
    templateParameters: []
    responses: []
  }
}

resource getRenderOAuth2RedirectPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2022-08-01' = {
  name: 'policy'
  parent: getRenderOAuth2RedirectOperation
  properties: {
    value: loadTextContent('./Policy/Anonymous/StandardEndpoint.xml')
    format: 'rawxml'
  }
}

resource getRenderOpenApiDocumentOperation 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'get-render-open-api-document'
  parent: anonymousApi
  properties: {
    displayName: 'RenderSwaggerDocument'
    method: 'GET'
    urlTemplate: '/openapi/V3.json'
    templateParameters: []
    responses: []
  }
}

resource getRenderOpenApiDocumentPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2022-08-01' = {
  name: 'policy'
  parent: getRenderOpenApiDocumentOperation
  properties: {
    value: loadTextContent('./Policy/Anonymous/StandardEndpoint.xml')
    format: 'rawxml'
  }
}

resource getRenderSwaggerDocumentOperation 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'get-render-swagger-Document'
  parent: anonymousApi
  properties: {
    displayName: 'RenderSwaggerDocument'
    method: 'GET'
    urlTemplate: '/swagger.json'
    templateParameters: []
    responses: []
  }
}

resource getRenderSwaggerDocumentPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2022-08-01' = {
  name: 'policy'
  parent: getRenderSwaggerDocumentOperation
  properties: {
    value: loadTextContent('./Policy/Anonymous/StandardEndpoint.xml')
    format: 'rawxml'
  }
}

resource getRenderSwaggerUIOperation 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'get-render-swagger-ui'
  parent: anonymousApi
  properties: {
    displayName: 'RenderSwaggerUI'
    method: 'GET'
    urlTemplate: '/swagger/ui'
    templateParameters: []
    responses: []
  }
}

resource getRenderSwaggerUIPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2022-08-01' = {
  name: 'policy'
  parent: getRenderSwaggerUIOperation
  properties: {
    value: loadTextContent('./Policy/Anonymous/StandardEndpoint.xml')
    format: 'rawxml'
  }
}
