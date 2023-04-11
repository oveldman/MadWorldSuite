param parentName string = 'madworld-api-anonymous'
param triggerName string = 'HttpTrigger'
param projectNamespace string = 'MadWorld.Backend.API.Anonymous'

resource azureFucntionParent 'Microsoft.Web/sites@2022-09-01' existing = {
  name: parentName
}

resource trigger 'Microsoft.Web/sites/functions@2022-09-01' = {
  name: triggerName
  parent: azureFucntionParent
  properties: {
    script_href: 'https://${parentName}.azurewebsites.net/admin/vfs/home/site/wwwroot/${projectNamespace}.dll'
    test_data_href: 'https://${parentName}.azurewebsites.net/admin/vfs/tmp/FunctionsData/${triggerName}}.dat'
    href: 'https://${parentName}.azurewebsites.net/admin/functions/${triggerName}}'
    config: {}
    invoke_url_template: 'https://${parentName}.net/api/${triggerName}'
    language: 'dotnet-isolated'
    isDisabled: false
  }
}
