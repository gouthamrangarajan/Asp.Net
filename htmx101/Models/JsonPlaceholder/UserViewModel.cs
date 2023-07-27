using System.Text.Json.Serialization;

namespace htmx101.Models.JsonPlaceholder;

public class UserViewModel{
    [JsonPropertyName("id")]
    public int Id {get;set;}

    [JsonPropertyName("name")]
    public string Name {get;set;} ="";

    [JsonPropertyName("username")]
    public string UserName {get;set;} ="";

    [JsonPropertyName("phone")]
    public string Phone {get;set;}="";

    [JsonPropertyName("website")]
    public string Website{get;set;}="";
}