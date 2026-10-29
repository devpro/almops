using System.Text.Json.Serialization;

namespace AlmOps.GitLabComponent.Infrastructure.RestApi.Dto;

public class DetailsDto
{
    [JsonPropertyName("custom_message")]
    public string? CustomMessage { get; set; }

    [JsonPropertyName("author_name")]
    public string? AuthorName { get; set; }

    [JsonPropertyName("author_email")]
    public string? AuthorEmail { get; set; }

    [JsonPropertyName("target_id")]
    public object? TargetId { get; set; }

    [JsonPropertyName("target_type")]
    public string? TargetType { get; set; }

    [JsonPropertyName("target_details")]
    public string? TargetDetails { get; set; }

    [JsonPropertyName("ip_address")]
    public string? IpAddress { get; set; }

    [JsonPropertyName("entity_path")]
    public string? EntityPath { get; set; }

    public string? Add { get; set; }

    public string? Change { get; set; }

    public string? From { get; set; }

    public string? To { get; set; }

    [JsonPropertyName("author_class")]
    public string? AuthorClass { get; set; }
}
