namespace WeatherForecast.Models

open System
open System.Collections.Generic



[<Class>]
type public InfoViewModel(title: string) =
    
    member val public Title: string = title with get, set
    member val public Description: string = String.Empty with get, set

    member val public ScriptFileNames: List<string> = List<string>() with get, set