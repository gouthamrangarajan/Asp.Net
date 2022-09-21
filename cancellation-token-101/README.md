### Cancellation Token 101

#### very useful to cancel a process/ resource access

##### sample to cancel a data generation in Weatherforecast

```C#
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
```

- To test it , hit the get url in browser https://localhost:7257/weatherforecast and close the browser tab within 4 seconds.
- OR if vscode and REST client extensions are available use test.http
- We can see the exception getting logged
