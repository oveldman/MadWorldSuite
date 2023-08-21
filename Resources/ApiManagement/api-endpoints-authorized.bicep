param apiManagementName string

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

resource getAccounts 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'get-accounts'
  parent: authorizedApi
  properties: {
    displayName: 'GetAccounts'
    method: 'GET'
    urlTemplate: '/Account'
    templateParameters: []
    responses: []
  }
}

resource getAccountsPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2022-08-01' = {
  name: 'policy'
  parent: getAccounts
  properties: {
    value: loadTextContent('./Policy/Authorized/StandardEndpoint.xml')
    format: 'rawxml'
  }
}

resource getAccount 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'get-account'
  parent: authorizedApi
  properties: {
    displayName: 'GetAccounts'
    method: 'GET'
    urlTemplate: '/Account/{id}'
    templateParameters: [
      {
        name: 'id'
        required: true
        values: []
        type: 'SecureString'
      }
    ]
    responses: []
  }
}

resource getAccountPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2022-08-01' = {
  name: 'policy'
  parent: getAccount
  properties: {
    value: loadTextContent('./Policy/Authorized/StandardEndpoint.xml')
    format: 'rawxml'
  }
}

resource getBlog 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'get-blog'
  parent: authorizedApi
  properties: {
    displayName: 'GetBlog'
    method: 'GET'
    urlTemplate: '/GetBlog/{id}'
    templateParameters: [
      {
        name: 'id'
        required: true
        values: []
        type: 'SecureString'
      }
    ]
    responses: []
  }
}

resource getBlogPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2022-08-01' = {
  name: 'policy'
  parent: getBlog
  properties: {
    value: loadTextContent('./Policy/Authorized/StandardEndpoint.xml')
    format: 'rawxml'
  }
}

resource getBlogs 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'get-blogs'
  parent: authorizedApi
  properties: {
    displayName: 'GetBlogs'
    method: 'GET'
    urlTemplate: '/GetBlogs/page/{page}'
    templateParameters: [
      {
        name: 'page'
        required: true
        values: []
        type: 'SecureString'
      }
    ]
    responses: []
  }
}

resource getBlogsPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2022-08-01' = {
  name: 'policy'
  parent: getBlogs
  properties: {
    value: loadTextContent('./Policy/Authorized/StandardEndpoint.xml')
    format: 'rawxml'
  }
}

resource getCurriculumVitae 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'get-curriculum-vitae'
  parent: authorizedApi
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
    value: loadTextContent('./Policy/Authorized/StandardEndpoint.xml')
    format: 'rawxml'
  }
}

resource getHealthCheckOperation 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'get-health-check'
  parent: authorizedApi
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
    value: loadTextContent('./Policy/Authorized/AnonymousEndpoint.xml')
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
    value: loadTextContent('./Policy/Authorized/AnonymousEndpoint.xml')
    format: 'rawxml'
  }
}

resource getPingWithUsernameOperation 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'get-ping-with-username'
  parent: authorizedApi
  properties: {
    displayName: 'PingWithUsername'
    method: 'GET'
    urlTemplate: '/PingWithUsername'
    templateParameters: []
    responses: []
  }
}

resource getPingWithUsernamePolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2022-08-01' = {
  name: 'policy'
  parent: getPingWithUsernameOperation
  properties: {
    value: loadTextContent('./Policy/Authorized/StandardEndpoint.xml')
    format: 'rawxml'
  }
}

resource getStatusOperation 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'get-status'
  parent: authorizedApi
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
    value: loadTextContent('./Policy/Authorized/AnonymousEndpoint.xml')
    format: 'rawxml'
  }
}

resource patchAccounts 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'patch-accounts'
  parent: authorizedApi
  properties: {
    displayName: 'PatchAccounts'
    method: 'PATCH'
    urlTemplate: '/Account'
    templateParameters: []
    responses: []
  }
}

resource patchAccountsPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2022-08-01' = {
  name: 'policy'
  parent: patchAccounts
  properties: {
    value: loadTextContent('./Policy/Authorized/StandardEndpoint.xml')
    format: 'rawxml'
  }
}

resource patchCurriculumVitae 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'patch-curriculum-vitae'
  parent: authorizedApi
  properties: {
    displayName: 'PatchCurriculumVitae'
    method: 'PATCH'
    urlTemplate: '/CurriculumVitae'
    templateParameters: []
    responses: []
  }
}

resource patchCurriculumVitaePolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2022-08-01' = {
  name: 'policy'
  parent: patchCurriculumVitae
  properties: {
    value: loadTextContent('./Policy/Authorized/StandardEndpoint.xml')
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
    value: loadTextContent('./Policy/Authorized/AnonymousEndpoint.xml')
    format: 'rawxml'
  }
}

resource getRenderOAuth2RedirectOperation 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'get-render-oAuth-2-Redirect'
  parent: authorizedApi
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
    value: loadTextContent('./Policy/Authorized/AnonymousEndpoint.xml')
    format: 'rawxml'
  }
}

resource getRenderOpenApiDocumentOperation 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'get-render-open-api-document'
  parent: authorizedApi
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
    value: loadTextContent('./Policy/Authorized/AnonymousEndpoint.xml')
    format: 'rawxml'
  }
}

resource getRenderSwaggerDocumentOperation 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'get-render-swagger-Document'
  parent: authorizedApi
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
    value: loadTextContent('./Policy/Authorized/AnonymousEndpoint.xml')
    format: 'rawxml'
  }
}

resource getRenderSwaggerUIOperation 'Microsoft.ApiManagement/service/apis/operations@2022-08-01' = {
  name: 'get-render-swagger-ui'
  parent: authorizedApi
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
    value: loadTextContent('./Policy/Authorized/AnonymousEndpoint.xml')
    format: 'rawxml'
  }
}
