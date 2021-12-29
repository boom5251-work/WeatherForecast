namespace WeatherForecast.Dependencies

open System.Collections.Generic
open System.IO
open System.Linq
open System.Text.Json
open WeatherForecast.Dependencies.Serializable



[<Interface>]
type public IConditionIconService =

    abstract member GetIconFilesByCode : uint16 -> List<string>
    abstract All : IEnumerable<List<string>> with get



[<Class>]
type public ConditionIconService() =

    let mutable conditions: List<WeatherCondition> = null


    do
        use streamReader: StreamReader = new StreamReader("Dependencies/Json/weatherConditions.json")
        conditions <- JsonSerializer.Deserialize<List<WeatherCondition>>(streamReader.BaseStream)
    

    interface IConditionIconService with

        // Получение названий svg-файлов иконок, соответсвующих коду погодных условий.
        member this.GetIconFilesByCode(conditionCode: uint16): List<string> =

            conditions.Single(fun condition -> condition.Code = conditionCode).SvgFilePaths.ToList()


        // Получение названий svg-файлов иконок для всех погодных условий.
        member val All: IEnumerable<List<string>> = 
            conditions.Select(fun condition -> condition.SvgFilePaths.ToList())
            with get