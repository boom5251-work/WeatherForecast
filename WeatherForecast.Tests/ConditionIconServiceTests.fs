namespace WeatherForecast.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open System.Collections.Generic
open System.IO
open System.Linq
open System.Reflection
open System.Text.Json
open WeatherForecast.Dependencies


[<TestClass>]
type public ConditionIconServiceTests() =

    let service: IConditionIconService = ConditionIconService()
    let allItems: IEnumerable<List<string>> = service.All


    member private this.GetTestJson(): JsonDocument =
        
        let assembly: Assembly = Assembly.GetExecutingAssembly()
        let resourceName: string = "WeatherForecast.Tests.TestData.conditions.json"

        use stream: Stream = assembly.GetManifestResourceStream(resourceName)
        use reader: StreamReader = new StreamReader(stream)
        let jsonFile: string = reader.ReadToEnd()

        JsonDocument.Parse(jsonFile)


    [<TestMethod>]
    member public this.GetAll_Count_GotAll(): unit =
        
        Assert.IsNotNull(allItems)
        Assert.IsTrue(allItems.Count() > 0)

        for item: List<string> in allItems do
            Assert.IsTrue(item.Count >= 2)


    [<TestMethod>]
    member public this.GetIcons_TestJson_GotAll(): unit = 
        
        let testDoc: JsonDocument = this.GetTestJson()

        for item in testDoc.RootElement.EnumerateArray() do

            let code: uint16 = uint16(item.GetProperty("code").GetInt32())
            let iconFileNames: List<string> = service.GetIconFilesByCode(code)

            Assert.IsNotNull(iconFileNames)
            Assert.IsTrue(iconFileNames.Count > 0)

