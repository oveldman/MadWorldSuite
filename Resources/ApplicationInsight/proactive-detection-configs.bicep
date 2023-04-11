param insightName string = 'madworld-api-anonymous'
param configName string = 'degradationindependencyduration'
param displayName string = 'Degradation in Dependency Duration'
param description string = 'Smart Detection rules notify you of performance anomaly issues.'
param helpUrl string = 'https://docs.microsoft.com/en-us/azure/application-insights/app-insights-proactive-performance-diagnostics'
param isHidden bool = false
param isInPreview bool = false
param supportsEmailNotifications bool = true
param isEnabled bool = true

resource applicationInsightResource 'Microsoft.Insights/components@2020-02-02' existing = {
  name: insightName
}

resource proactiveDetectionConfigs 'Microsoft.Insights/components/ProactiveDetectionConfigs@2018-05-01-preview' = {
  name: '${configName}-${insightName}'
  parent: applicationInsightResource
  properties: {
    RuleDefinitions: {
      Name: configName
      DisplayName: displayName
      Description: description
      HelpUrl: helpUrl
      IsHidden: isHidden
      IsEnabledByDefault: isEnabled
      IsInPreview: isInPreview
      SupportsEmailNotifications: supportsEmailNotifications
    }
    Enabled: isEnabled
    SendEmailsToSubscriptionOwners: true
    CustomEmails: []
  }
}
