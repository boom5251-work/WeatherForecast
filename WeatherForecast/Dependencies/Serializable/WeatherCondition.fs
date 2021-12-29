namespace WeatherForecast.Dependencies.Serializable

open System
open System.Text.Json.Serialization



[<Class; Serializable>]
type public WeatherCondition() =
    
    [<JsonPropertyName("code")>]
    member val public Code: uint16 = 0us with get, set
    [<JsonPropertyName("svg_files")>]
    member val public SvgFilePaths: string[] = null with get, set