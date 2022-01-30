namespace WeatherForecast.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open System
open System.Collections.Generic
open System.Globalization
open System.IO
open System.Linq
open System.Reflection
open System.Text.Json
open WeatherForecast.Data
open WeatherForecast.Models


[<TestClass>]
type public ModelerTests() =
    
    let timePattern: string = "h:mm tt"
    let dateTimePattern: string = "yyyy-MM-dd H:mm"


    member private this.GetTestJson(fileName: string): JsonDocument =
        
        let assembly: Assembly = Assembly.GetExecutingAssembly()
        let resourceName: string = $"WeatherForecast.Tests.TestData.{fileName}"

        use stream: Stream = assembly.GetManifestResourceStream(resourceName)
        use reader: StreamReader = new StreamReader(stream)
        let jsonFile: string = reader.ReadToEnd()

        JsonDocument.Parse(jsonFile)


    [<TestMethod>]
    member public this.CreateAstronomyViewModel_TestJson_Equal(): unit =

        let sunriseDateTime: DateTime = DateTime.ParseExact("08:27 AM", timePattern, CultureInfo.InvariantCulture)
        let sunsetDateTime: DateTime = DateTime.ParseExact("04:59 PM", timePattern, CultureInfo.InvariantCulture)

        let expected: AstronomyViewModel = AstronomyViewModel()
        expected.Sunrise <- TimeOnly.FromDateTime(sunriseDateTime)
        expected.Sunset <- TimeOnly.FromDateTime(sunsetDateTime)


        let testDoc: JsonDocument = this.GetTestJson("astronomy.json")
        let actual: AstronomyViewModel = Modeler.createAstronomyViewModel testDoc


        Assert.IsTrue(expected.Sunrise = actual.Sunrise && expected.Sunset = actual.Sunset)
        Assert.IsTrue(actual.IsInited)


    [<TestMethod>]
    member public this.CreateForecastViewModel_TestJson_Equals(): unit = 
        
        let testDoc: JsonDocument = this.GetTestJson("forecast.json")
        let actuals: List<ForecastViewModel> = Modeler.createForecastViewModels testDoc

        let lastActual: ForecastViewModel = actuals.Last()
        let iLastActual: IConditionViewModel = lastActual :> IConditionViewModel

        
        let dateTime: DateTime = 
            DateTime.ParseExact("2022-01-31 23:00", dateTimePattern, CultureInfo.InvariantCulture)

        Assert.AreEqual(dateTime, lastActual.DateTime)
        Assert.AreEqual(false, iLastActual.IsDay)
        Assert.AreEqual(-1.9, iLastActual.Temperature)
        Assert.AreEqual(-6.3, iLastActual.FeelsLikeTemperature)
        Assert.AreEqual(1225us, iLastActual.WeatherCondition)


    [<TestMethod>]
    member public this.CreateCurrentWeatherViewModel(): unit =

        let testDoc: JsonDocument = this.GetTestJson("current.json")
        let actual: CurrentWeatherViewModel = Modeler.createCurrentWeatherViewModel testDoc


        Assert.AreEqual("Russia", actual.Country)
        Assert.AreEqual("Moscow City", actual.Region)
        Assert.AreEqual("Moscow", actual.LocName)
        Assert.AreEqual("SE", actual.WindDirection)
        Assert.AreEqual(22.0, actual.WindSpeed)