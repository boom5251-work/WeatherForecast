namespace WeatherForecast.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open System.Text.Json
open WeatherForecast.Data


[<TestClass>]
type public WeatherApiTests() =
    
    let apiKey: string = "d4b9d636ed454ab68e4121950212512"
    let webSite: string = "https://api.weatherapi.com"
    let methods: string[] = [| "current"; "forecast"; "astronomy" |]



    member private this.SendHttpRequest(currentUrl: string): unit =
        
        let actual: JsonDocument = WeatherApi.sendHttpGetRequest currentUrl

        Assert.IsNotNull(actual)



    [<TestMethod>]
    member public this.SendHttpRequest_Moscow_HasJson(): unit =
        
        let cityName: string = "Moscow"

        for method in methods do
            
            let currentUrl: string = $"{webSite}/v1/{method}.json?key={apiKey}&q={cityName}"
            this.SendHttpRequest currentUrl



    [<TestMethod>]
    member public this.SendHttpRequest_MoscowCoords_HasJson(): unit =
        
        let coords: string = "55.755819,37.617644"
        
        for method in methods do
            
            let currentUrl: string = $"{webSite}/v1/{method}.json?key={apiKey}&q={coords}"
            this.SendHttpRequest currentUrl