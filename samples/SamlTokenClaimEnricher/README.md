# Demo

Install Azurite

[Use the Azurite emulator for local Azure Storage development](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite)

Install Azurite

[Install and run Azurite emulator](https://learn.microsoft.com/en-us/azure/storage/common/storage-install-azurite)

```bash
npm install -g azurite
```

The goal is to return this kind of response:

```json
{
  "data": {
    "@odata.type": "microsoft.graph.onTokenIssuanceStartResponseData",
    "actions": [
      {
        "@odata.type": "microsoft.graph.tokenIssuanceStartCustomExtensionHandlerStep",
        "stepModifier": {
          "@odata.type": "microsoft.graph.customExtensionOverwriteAllowedCustomClaimsUpdateStepModifier",
          "claims": [
            {
              "claimType": "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/githubteams",
              "values": [
                "Engineering",
                "Security-Ops"
              ]
            }
          ]
        }
      }
    ]
  }
}
```
