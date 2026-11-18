# Demo

Install Azurite

[Use the Azurite emulator for local Azure Storage development](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite)

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
        "@odata.type": "microsoft.graph.tokenIssuanceStart.provideClaimsForToken",
        "claims": {
          "UserRole": "SecurityAuditor",
          "ProjectCode": "Alpha-Sandbox",
          "IsDemoUser": "true",
          "CustomGroup": [
            "PS_EMEA",
            "CS_EMEA",
            "Revenue"
          ]
        }
      }
    ]
  }
}
```
