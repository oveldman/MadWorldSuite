param applicationInsightName string
param smartDectectionName string

resource applicationInsight 'Microsoft.Insights/components@2020-02-02' existing = {
  name: applicationInsightName
}

resource actionGroup 'Microsoft.Insights/actionGroups@2023-01-01' existing = {
  name: smartDectectionName
}

resource smartdetectoralertrules 'microsoft.alertsManagement/smartDetectorAlertRules@2021-04-01' = {
  name: format('failure anomalies - {0}', applicationInsightName)
  location: 'global'
  properties: {
    description: 'Failure Anomalies notifies you of an unusual rise in the rate of failed HTTP requests or dependency calls.'
    state: 'Enabled'
    severity: 'Sev3'
    frequency: 'PT1M'
    detector: {
      id: 'FailureAnomaliesDetector'
    }
    scope: [
      applicationInsight.id
    ]
    actionGroups: {
      groupIds: [
        actionGroup.id
      ]
    }
  }
}
