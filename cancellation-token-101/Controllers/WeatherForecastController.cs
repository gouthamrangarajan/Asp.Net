using Microsoft.AspNetCore.Mvc;

namespace cancellation_token_101.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>?> Get(CancellationToken token)
    {
        try{
            //RG the below can be anything e.g fetching records from db, fetching records from another api
            //almost all objects/library used in this scenario accepts Cancellation token 
            // e.g httpclient/ dapper
            await Task.Delay(4000,token);
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        catch(Exception e){
            _logger.LogError(e,"Exception occured in GetWeatherForecast");
            return null;
        }
    }
    [HttpPost(Name="PostWeatherForeCast")]
    public async Task<IEnumerable<WeatherForecast>?> Post([FromBody]PostRequest request,CancellationToken token)
    //RG for post most proabably a request will be sent in body/any other means preferred and cancellationToken will be a separate object
    {
        try{
            await Task.Delay(4000,token);
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        catch(Exception e){
            _logger.LogError(e,$"Exception occured in PostWeatherForecast {request.Id}");
            return null;
        }
    }
}

public class PostRequest
{
    public int Id {get;set;}
}