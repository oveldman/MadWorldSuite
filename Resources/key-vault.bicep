param location string

resource vault 'Microsoft.KeyVault/vaults@2023-02-01' = {
  name: 'MadWorld-KeyVault'
  location: location
  properties: {
    sku: {
      family: 'A'
      name: 'standard'
    }
    tenantId: '01964ed8-e50c-4f9b-b13e-96e3cf7a6c51'
    accessPolicies: []
    enabledForDeployment: false
    enabledForDiskEncryption: false
    enabledForTemplateDeployment: true
    enableSoftDelete: true
    softDeleteRetentionInDays: 90
    enableRbacAuthorization: true
    vaultUri: 'https://madworld-keyvault.vault.azure.net/'
    provisioningState: 'Succeeded'
    publicNetworkAccess: 'Enabled'
  }
}

resource azureAdClientSecret 'Microsoft.KeyVault/vaults/secrets@2023-02-01' = {
  name: 'AzureAD-ClientSecret'
  parent: vault
  properties: {
    attributes: {
      enabled: true
    }
  }
}
