using System.Net;
using System.Text.Json.Serialization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace SamlTokenClaimEnricher;

public class TokenClaimsEnricher(ILoggerFactory loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<TokenClaimsEnricher>();

    [Function("TokenClaimsEnricher")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        _logger.LogInformation("Entra ID called the Token Enrichment API.");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");

        // Hard-coded string to prevent .NET from stripping the @odata.type keys
        string json = @"
{
  ""data"": {
    ""@odata.type"": ""microsoft.graph.onTokenIssuanceStartResponseData"",
    ""actions"": [
      {
        ""@odata.type"": ""microsoft.graph.tokenIssuanceStart.provideClaimsForToken"",
        ""claims"": {
          ""UserRole"": ""SecurityAuditor"",
          ""ProjectCode"": ""Alpha-Sandbox"",
          ""IsDemoUser"": ""true"",
          ""CustomGroup"": [
            ""PS_EMEA"",
            ""CS_EMEA"",
            ""Revenue""
          ]
        }
      }
    ]
  }
}";

        await response.WriteStringAsync(json);
        return response;
    }
}

public class RootResponse
{
    [JsonPropertyName("data")]
    public ResponseData Data { get; set; }
}

public class ResponseData
{
    [JsonPropertyName("@odata.type")]
    public string OdataType { get; set; }

    [JsonPropertyName("actions")]
    public ActionData[] Actions { get; set; }
}

public class ActionData
{
    [JsonPropertyName("@odata.type")]
    public string OdataType { get; set; }

    [JsonPropertyName("claims")]
    public AdditionalClaims Claims { get; set; }
}

public class AdditionalClaims
{
    public string UserRole { get; set; }
    public string ProjectCode { get; set; }
    public string IsDemoUser { get; set; }
}
