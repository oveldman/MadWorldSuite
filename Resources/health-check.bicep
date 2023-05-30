param location string = resourceGroup().location
param name string = 'health check-madworld-api-authorized'
param applicationInsightName string = 'madworld-api-authorized'
param healthEndpoint string = 'https://api.mad-world.nl/authorized/healthcheck'

resource applicationInsight 'Microsoft.Insights/components@2020-02-02' existing = {
  name: applicationInsightName
}

resource webTests 'Microsoft.Insights/webtests@2022-06-15' = {
  name: name
  location: location
  tags: {
    'hidden-link:${applicationInsight.id}': 'Resource'
  }
  properties: {
    SyntheticMonitorId: name
    Name: 'Health Check'
    Enabled: false
    Frequency: 86400
    Timeout: 120
    Kind: 'standard'
    RetryEnabled: true
    Locations: [
      {
        Id: 'emea-nl-ams-azr'
      }
      {
        Id: 'apac-jp-kaw-edge'
      }
      {
        Id: 'us-il-ch1-azr'
      }
      {
        Id: 'emea-fr-pra-edge'
      }
      {
        Id: 'emea-se-sto-edge'
      }
    ]
    Request: {
      RequestUrl: healthEndpoint
      HttpVerb: 'GET'
      ParseDependentRequests: false
    }
    ValidationRules: {
      ExpectedHttpStatusCode: 200
      IgnoreHttpStatusCode: false
      ContentValidation: {
        ContentMatch: 'Healthy'
        IgnoreCase: false
        PassIfTextFound: true
      }
      SSLCheck: true
      SSLCertRemainingLifetimeCheck: 7
    }
  }
}

resource metricAlerts 'Microsoft.Insights/metricAlerts@2018-03-01' = {
  name: name
  location: 'global'
  tags: {
    'hidden-link:${applicationInsight.id}': 'Resource'
    'hidden-link:${webTests.id}': 'Resource'
  }
  properties: {
    description: 'Automatically created alert rule for availability test ${name}'
    severity: 1
    enabled: false
    scopes: [
      webTests.id
      applicationInsight.id
    ]
    evaluationFrequency: 'PT1M'
    windowSize: 'PT5M'
    criteria: {
      webTestId: webTests.id
      componentId: applicationInsight.id
      failedLocationCount: 2
      'odata.type': 'Microsoft.Azure.Monitor.WebtestLocationAvailabilityCriteria'
    }
    actions: []
  }
}